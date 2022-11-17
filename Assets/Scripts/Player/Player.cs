using UnityEngine;

public class Player : MonoBehaviour
{
    public int coins;
    public int anchors;
    public float score;

    public void SavePlayer()
    {
        //Get player stored coins
        //Add the coins obtained
        //Save player data;
        float previousScore = GameManager.SharedInstance.GetDistance();
        PlayerData data = SaveSystem.LoadPlayer();
        coins += data.coins;
        anchors += data.anchors;
        score = data.score > previousScore ? data.score : previousScore;
        SaveSystem.SavePlayer(this);
    }

    //Player
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        coins = data.coins;
        anchors = data.anchors;
        score = data.score;
    }
    public void ResetData()
    {
        coins = 0;
        anchors = 0;
        score = 0;
    }
    
    //Coins
    public void AddCoin()
    {
        coins++;
    }
    public int GetCoins()
    {
        return coins;
    }
    
    //Anchors
    public void AddAnchor()
    {
        anchors++;
    }
    public int GetAnchors()
    {
        return anchors;
    }

    #region Develop Options
    
    public void AddMoreCoins()
    {
        coins += 2000;
        SaveSystem.SavePlayer(this);
    }
    public void AddMoreAnchors()
    {
        anchors += 100;
        SaveSystem.SavePlayer(this);
    }
    public void DeletePlayerData()
    {
        SaveSystem.Delete();
    }
    public void InitializedData()
    {
        SaveSystem.InitializedData();
    }
    
    #endregion 
}
