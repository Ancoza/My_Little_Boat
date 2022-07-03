using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int coins;
    public float score;

    // void Update()
    // {
    //      if (GameManager.SharedInstance.currentGameState == GameState.InGame)
    //      {
    //          score += Time.deltaTime;
    //      }
    // }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        coins = data.coins;
        score = data.score;
    }

    public void AddCoin()
    {
        coins++;
    }
    public void AddCoins(int previousCoins)
    {
        coins += previousCoins;
    }
    public int GetCoins()
    {
        return coins;
    }
    public float GetScore()
    {
        return score;
    }

    public void SumCoins()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            SavePlayer();
        }
        else
        {
            coins += data.coins;
            SavePlayer();
        }

    }
}
