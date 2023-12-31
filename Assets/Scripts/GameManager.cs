using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState{
    Menu,
    InGame,
    GameOver
}
public enum GameController
{
    Touch,
    Gyroscope
}

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;
    
    [Header("Settings")]
    public GameState currentGameState;
    public GameController currentGameController;
    private float gameVelocity = 0;
    readonly float gameTimeScale = 5f;
    private readonly float gameDifficultIncrement = 0.20f;
    
    [Header("UI")] 
    public TextMeshProUGUI tmpCoins;
    public TextMeshProUGUI tmpScore;
    public TextMeshProUGUI tmpCoinsMain;
    public TextMeshProUGUI tmpScoreMain;
    public TextMeshProUGUI tmpAnchorsMain;
    public TextMeshProUGUI finishCoins;
    
    [Header("Player")]
    public List<Boat> allBoats;
    private Player player;
    private float distance = 0;
    
    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }
    private void Start()
    {
        StartGameSettings();
    }
    private void Update()
    {
        UpdateUI();
    }

    private void StartGameSettings()
    {
        currentGameState = GameState.Menu;
        Controller();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.LoadPlayer();
    }
    void UpdateUI()
    {
        if (currentGameState == GameState.InGame)
        {
            distance += Time.deltaTime;
            tmpCoins.text = $"{player.GetCoins()}";
            tmpScore.text = $"{distance.ToString("0000")}";
            finishCoins.text = $"{player.GetCoins()}";
        }
        else
        {
            player.LoadPlayer();
            tmpCoinsMain.text = $"{player.GetCoins()}";
            tmpAnchorsMain.text = $"{player.GetAnchors()}";
            tmpScoreMain.text = $"{player.score.ToString("0000")}";
        }
    }
    void IncrementDifficult()
    {
        gameVelocity += gameDifficultIncrement;
    }
    public float GetGameVelocity()
    {
        return gameVelocity;
    }
    public float GetDistance()
    {
        return distance;
    }

    //Get player boat
    public Boat GetBoat()
    {
        Boat selectedBoat = null;
        foreach (Boat boat in allBoats)
        {
            if (boat.onUse)
            {
                selectedBoat = boat;
            }
        }
        return selectedBoat;
    }
    
    //Set GameStates
    #region GameStates
    public void GameOver()
    {
        SetGameStates(GameState.GameOver);
    }
    public void InGame()
    {
        SetGameStates(GameState.InGame);
    }
    public void Menu() 
    {
        SetGameStates(GameState.Menu);   
    }
    private void SetGameStates(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.Menu:
                MenuManager.sharedInstance.MainMenu();
                break;
            case GameState.InGame:
                MenuManager.sharedInstance.InGameMenu();
                Environment.SharedInstance.GenerateInitialBuildings();
                Spawner.SharedInstance.StartSpawner();
                gameVelocity = 7;
                InvokeRepeating(nameof(IncrementDifficult), gameTimeScale, gameTimeScale);
                player.ResetData();
                break;
            case GameState.GameOver:
                StartCoroutine(nameof(LoadMain));
                gameVelocity = 0;
                //SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }
        currentGameState = newGameState;
    }
    #endregion

    /*
        Get player Controller
        0 = Gyroscope (D)
        1 = Touch
    */
    #region Controller
    void Controller()
    {
        if (!PlayerPrefs.HasKey("GameController"))
        {
            PlayerPrefs.GetInt("GameController", 0);
            SetGameController(GameController.Gyroscope);
        }
        else if (PlayerPrefs.GetInt("GameController") == 1)
        {
            SetGameController(GameController.Touch); 
        }else if (PlayerPrefs.GetInt("GameController") == 0)
        {
            SetGameController(GameController.Gyroscope);
        }
    }
    void SetGameController(GameController newGameController)
    {
        if (newGameController == GameController.Gyroscope)
        {
            PlayerPrefs.SetInt("GameController",0);
        }else if (newGameController == GameController.Touch)
        {
            PlayerPrefs.SetInt("GameController",1);
        }
        currentGameController = newGameController;
    }
    #endregion
    
    IEnumerator LoadMain()
    {
        yield return new WaitForSeconds(2);
        MenuManager.sharedInstance.GameOver();
        yield return new WaitForSeconds(2);
        MenuManager.sharedInstance.CloseGameOver();
        SceneManager.LoadScene("Scenes/game", LoadSceneMode.Single);
    }
}
