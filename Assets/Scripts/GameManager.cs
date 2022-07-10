using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameState currentGameState;
    public GameController currentGameController;

    public Camera mainCamera;
    
    public List<Boat> AllBoats;

    [SerializeField]
    private float _gameVelocity;

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
        _gameVelocity = 10.0f;
        currentGameState = GameState.InGame;
        Controller();
    }
    

    public float GetGameVelocity()
    {
        return _gameVelocity;
    }

    public float GetDistance()
    {
        return _distance;
    }

    private void Update()
    {
        _distance += Time.deltaTime;
    }

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
    
    //Game States
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
    IEnumerator LoadMain()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scenes/Main", LoadSceneMode.Single);
    }
}
