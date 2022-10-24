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
    private float _gameVelocity;
    float _gameTimeScale = 5f;
    private float _gameDifficultIncrement = 0.20f;
    
    [Header("UI")] 
    public TextMeshProUGUI tmpCoins;
    public TextMeshProUGUI tmpScore;
    
    [Header("Player")]
    public List<Boat> AllBoats;
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
        _gameVelocity = 5;
        currentGameState = GameState.InGame;
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Controller();
        InvokeRepeating(nameof(IncrementDifficult) , _gameTimeScale, _gameTimeScale);
    }
    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (currentGameState == GameState.InGame)
        {
            float multiplicator = 1.5f;
            _distance += Time.deltaTime * multiplicator;
            tmpCoins.text = "" + _player.GetCoins();
            tmpScore.text = "" + _distance.ToString("0000");
        }
    }
    void IncrementDifficult()
    {
        _gameVelocity += _gameDifficultIncrement;
        Debug.Log("Incrementando Dificultad");
    }
    public float GetGameVelocity()
    {
        return _gameVelocity;
    }

    //Get player boat
    public Boat GetBoat()
    {
        Boat boat = null;
        foreach (Boat boats in AllBoats)
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
        SetGameState(GameState.GameOver);
    }
    public void InGame()
    {
        SetGameState(GameState.InGame);
    }
    public void Menu() 
    {
        SetGameState(GameState.Menu);   
    }
    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.Menu)
        {
            
        }else if (newGameState == GameState.InGame)
        {
            
        }else if (newGameState == GameState.GameOver)
        {
            MenuGame.SharedInstance.HideGameMenu();
            _gameVelocity = 0;
            StartCoroutine(nameof(LoadMain));
        }
        currentGameState = newGameState;
    }

    #endregion
    
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
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scenes/Main", LoadSceneMode.Single);
    }
}
