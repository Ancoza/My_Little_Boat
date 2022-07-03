using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    public GameObject boat;
    public Boat currentBoat;
    public int currentIndex = 0;

    [SerializeField] private List<Boat> allBoats;
    
    [SerializeField]
    private TextMeshProUGUI coinCounter,boatPrice;

    public Player player;
    public PlayerData playerData;

    public Button btnBuy;

    private void Start()
    {
        currentBoat = allBoats[0];
        player.LoadPlayer();
        playerData = SaveSystem.LoadPlayer();
        Debug.Log(player.coins);
        LoadBoat();
    }

    
    void Update ()
    {
        coinCounter.text = "" + player.GetCoins();
        
        
        boat.transform.Rotate(Vector3.up * 15 * Time.deltaTime);

        currentBoat = allBoats[currentIndex];
        boatPrice.text = "$ " + currentBoat.GetComponent<Boat>().price;
        
        #if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextBoat();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousBoat();
        }
        #endif
        
        
    }


    public void NextBoat()
    {
        currentIndex += 1;
        if (currentIndex < allBoats.Count)
        {
            Destroy(boat.transform.GetChild(0).gameObject); 
            currentBoat = Instantiate(allBoats[currentIndex], boat.transform,false);
            //currentBoat.transform.position = new Vector3(0, 0, 0);
        }else if (currentIndex >= allBoats.Count)
        {
            currentIndex -= 1;
        }
    }
    
    public void PreviousBoat()
    {
        currentIndex -= 1;
        if (currentIndex >= 0)
        {
            Destroy(boat.transform.GetChild(0).gameObject); 
            currentBoat = Instantiate(allBoats[currentIndex], boat.transform,false);
            //currentBoat.transform.position = new Vector3(0, 0, 0);
        }else if (currentIndex < 0)
        {
            currentIndex += 1;
        }

    }
    
    private void LoadBoat()
    {
        currentBoat = Instantiate(allBoats[currentIndex], boat.transform,false);
        //currentBoat.transform.position = new Vector3(0, 0, 0);
        
    }

    public void BuyBoat()
    {
        if (!currentBoat.isUnlock && player.coins >= currentBoat.price)
        {
            player.coins -= currentBoat.price;
            currentBoat.isUnlock = true;
            foreach (Boat boatt in allBoats)
            {
                boatt.onUse = false;
            }
            currentBoat.onUse = true;

            Debug.Log(player.coins);
            Debug.Log(player.score);
            player.SavePlayer();
            UpdatePlayer();
            Debug.Log("You have a new Boat");
        }
        else
        {
            Debug.Log("You don't have money");
        }
    }

    void UpdatePlayer()
    {
        player.LoadPlayer();
    }
    public void CloseShop()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
