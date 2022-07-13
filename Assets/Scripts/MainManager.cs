using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public TextMeshProUGUI maxScore;
    public Player player;
    
    public List<Boat> allBoats;
    public GameObject boatParent;
    
    [SerializeField]
    private TextMeshProUGUI coinCounter;
    void Start()
    {
        player.LoadPlayer();
        maxScore.text = "Max Score: " + player.score.ToString("0000");
        Instantiate(GetBoat().gameObject, boatParent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        coinCounter.text = "" + player.GetCoins();
        maxScore.text = "Max Score: " + player.score.ToString("0000");
        boatParent.transform.Rotate(Vector3.up * 15 * Time.deltaTime);
    }
    
    public Boat GetBoat()
    {
        Boat boat = null;
        foreach (Boat boats in allBoats)
        {
            if (boats.onUse)
            {
                boat = boats;
            }
        }

        return boat;
    }
}
