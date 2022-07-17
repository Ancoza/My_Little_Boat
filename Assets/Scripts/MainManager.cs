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
        maxScore.text = "" + player.score.ToString("0000");
        
        var b = Instantiate(GetBoat().gameObject, boatParent.transform,false);
        b.transform.parent = boatParent.transform;
        b.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        coinCounter.text = "" + player.GetCoins();
        maxScore.text = "" + player.score.ToString("0000");
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
