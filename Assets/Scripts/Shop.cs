using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private int coins;
    private float score;

    private PlayerData PD;
    [SerializeField]
    private TextMeshProUGUI coinCounter;

    private void Start()
    {
        PD = SaveSystem.LoadPlayer();
    }

    void Update ()
    {
        coins = PD.coins;
        coinCounter.text = "" + coins ;
    }

}
