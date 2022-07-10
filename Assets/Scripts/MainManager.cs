using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public TextMeshProUGUI maxScore;
    public Player player;
    void Start()
    {
        player.LoadPlayer();
        maxScore.text = "Max Score: " + player.score.ToString("0000");
    }

    // Update is called once per frame
    void Update()
    {
        maxScore.text = "Max Score: " + player.score.ToString("0000");
    }
}
