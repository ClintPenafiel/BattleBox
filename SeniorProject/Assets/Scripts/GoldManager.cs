using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;
    public int startingGold = 0;
    public int currentGold = 0;
    public UnityEngine.UI.Text goldText;

    void Start()
{
    if (instance == null)
    {
        instance = this; 
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }

    currentGold = startingGold;
    goldText = GameObject.Find("GoldText").GetComponent<Text>();
    UpdateGoldText();
}

    public void DepositGold(int amount)
    {
        currentGold += amount;
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        goldText.text = "Gold: " + currentGold.ToString();
    }
}