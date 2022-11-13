using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coins;
    public float score;

    public PlayerData()
    {
        coins = 0;
        score = 0;
    }
    
    public PlayerData(Player player)
    {
        coins = player.coins;
        score = player.score;
    }
    
}