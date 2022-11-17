using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coins;
    public int anchors;
    public float score;

    public PlayerData()
    {
        coins = 0;
        anchors = 0;
        score = 0;
    }
    
    public PlayerData(Player player)
    {
        coins = player.coins;
        anchors = player.anchors;
        score = player.score;
    }
    
}