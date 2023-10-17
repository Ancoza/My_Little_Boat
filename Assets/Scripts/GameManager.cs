using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;

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
    private float gameVelocity;
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
    private Player _player;
    private float _distance;
    
    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }
    private void Start()
    {
        //CreateData();
        gameVelocity = 7;
        _distance = 0;
        currentGameState = GameState.Menu;
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Controller();
        _player.LoadPlayer();
    }
    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (currentGameState == GameState.InGame)
        {
            _distance += Time.deltaTime;
            tmpCoins.text = "" + _player.GetCoins();
            tmpScore.text = "" + _distance.ToString("0000");
            finishCoins.text = "" + _player.GetCoins();
        }
        else
        {
            _player.LoadPlayer();
            tmpCoinsMain.text = "" + _player.GetCoins();
            tmpAnchorsMain.text = "" + _player.GetAnchors();
            tmpScoreMain.text = "" + _player.score.ToString("0000");
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
        return _distance;
    }

    //Get player boat
    public Boat GetBoat()
    {
        Boat boat = null;
        foreach (Boat boats in allBoats)
        {
            if (boats.onUse)
            {
                boat = boats;
            }
        }
        return boat;
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
    #endregion
    
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
                InvokeRepeating(nameof(IncrementDifficult), gameTimeScale, gameTimeScale);
                _player.ResetData();
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
    
    //Get player Controller
    #region Controller
    void SetGameController(GameController newGameController)
    {
        if (newGameController == GameController.Gyroscope)
        {
            PlayerPrefs.SetInt("GameController",0);
            //mainCamera.GetComponent<TouchController>().enabled = false;
        }else if (newGameController == GameController.Touch)
        {
            PlayerPrefs.SetInt("GameController",1);
            //mainCamera.GetComponent<TouchController>().enabled = true;
        }
        currentGameController = newGameController;
    }
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
