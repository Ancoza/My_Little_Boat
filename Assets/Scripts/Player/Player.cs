using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int coins;
    public float score;

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

    public void NewData()
    {
        coins = 0;
        score = 0;
    }
    public void AddCoin()
    {
        coins++;
    }
    public int GetCoins()
    {
        return coins;
    }
    
    public void AddScore(float previousScore)
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data.score > previousScore)
        {
            score = data.score;
        }
        else
        {
            score = previousScore;
        }
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
