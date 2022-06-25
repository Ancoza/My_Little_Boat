using System;
using UnityEngine;

public enum GameState{
    Menu,
    InGame,
    GameOver
}
public class GameManager : MonoBehaviour
{

    public static GameManager SharedInstance;
    public GameState currentGameState  = GameState.Menu;

    [SerializeField]
    private float _gameVelocity;
    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }

    private void Start()
    {
        _gameVelocity = 5.0f;
    }

    void Update()
    {
        
    }

    public float GetGameVelocity()
    {
        return _gameVelocity;
    }
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
            
        }
        currentGameState = newGameState;
    }
}
