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
    public TextMeshProUGUI tmpCoinsMain;
    public TextMeshProUGUI tmpScoreMain;
    public TextMeshProUGUI tmpAnchorsMain;
    public TextMeshProUGUI finishCoins;
    
    [Header("Player")]
    public List<Boat> AllBoats;
    private Player _player;
    private float _distance;

    [Header("Buildings")] private Spawner _spawner;

    [SerializeField]
    private Initializeads ads;
    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }
    private void Start()
    {
        SaveSystem.InitializedData();
        //CreateData();
        _gameVelocity = 7;
        _distance = 0;
        currentGameState = GameState.Menu;
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
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
            //float multiplicator = 1.5f;
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
        _gameVelocity += _gameDifficultIncrement;
        //Debug.Log("Incrementando Dificultad");
    }
    public float GetGameVelocity()
    {
        return _gameVelocity;
    }

    public float GetDistance()
    {
        return _distance;
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
    #endregion
    
    
    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.Menu)
        {
            MenuManager.SharedInstance.MainMenu();
        }else if (newGameState == GameState.InGame)
        {
            ads.LoadBanner();
            MenuManager.SharedInstance.InGame();
            Environment.SharedInstance.GenerateInitialBuildings();
            InvokeRepeating(nameof(IncrementDifficult) , _gameTimeScale, _gameTimeScale);
            _player.ResetData();
            _spawner.StartSpawner();
            
        }else if (newGameState == GameState.GameOver)
        {
            StartCoroutine(nameof(LoadMain));
            _gameVelocity = 0;
            //SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
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
        MenuManager.SharedInstance.GameOver();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scenes/game", LoadSceneMode.Single);
        MenuManager.SharedInstance.CloseGameOver();
    }
}
