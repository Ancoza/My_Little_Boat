using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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

    public Toggle controllerGyro;
    public Toggle controllerTouch;
    public Slider generalVolume;
    public Toggle volumeFX;
    public Toggle volumeMusic;

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
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.LoadPlayer();
    }
    public void OpenSettings()
    {
        DataSettings settings = new DataSettings();
        settings = SaveSettings.LoadGameSettings();
        Debug.Log($"Open Settings Controller {settings.GetController()} Music: {settings.GetVolumeMusic()} FX: {settings.GetVolumeFX()} ");

        currentGameController = settings.GetController();
        if(currentGameController == GameController.Gyroscope) 
        { 
            controllerGyro.isOn = true;
        }
        else
        {
            controllerTouch.isOn = true;
        }
        generalVolume.value = settings.GetGeneralVolume();
        volumeMusic.isOn = settings.GetVolumeMusic();
        volumeFX.isOn = settings.GetVolumeFX();
    }
    public void CloseSettings()
    {
        Settings settings = new Settings();

        Debug.Log($"Close Settings Music: {volumeMusic.isOn} FX: {volumeFX.isOn} ");

        if (controllerTouch.isOn)
        {
            SetGameController(GameController.Touch);
        }
        else
        {
            SetGameController(GameController.Gyroscope);
        }

        settings.SetController(currentGameController);
        settings.SetGeneralVolume(generalVolume.value);
        settings.SetVolumeMusic(volumeMusic.isOn);
        settings.SetVolumeFX(volumeFX.isOn);
        settings.SaveGameSettigns();
        Debug.Log($"Close Settings Controller {settings.GetController()} Music: {settings.GetVolumeMusic()} FX: {settings.GetVolumeFX()} ");
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
