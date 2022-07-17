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
        CreateData();
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

    public void CreateData()
    {
        if (SaveSystem.LoadPlayer() == null)
        {
            Debug.Log("NUll");
            player.SavePlayer();
        }
        else
        {
            Debug.Log("Load Player");
            player.LoadPlayer();
        }
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
