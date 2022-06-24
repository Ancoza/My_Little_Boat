using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int coins;
    public float score;

    public TextMeshProUGUI Tscore, Tcoins;
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Ready");
            coins += 1;
        }

        if (other.CompareTag("Enemy"))
        {
            SavePlayer();
            SceneManager.UnloadSceneAsync("Game");
            MenuManager.SharedInstance.BackMain();
        }
    }

    private void Update()
    {
        score += Time.deltaTime;
        Tscore.text = "" + score.ToString("F2");

        Tcoins.text = "" + coins;
    }
}
