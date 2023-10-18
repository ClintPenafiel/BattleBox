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
//it should add all the gold that the gatherer has to the base when it reaches the base
    public void DepositGold(int gold)
    {
        currentGold += gold;
        UpdateGoldText(); //updates the gold text when the gold is deposited, so the player can see how much gold they have, next we need to make it so that the gold is added to the base by the gatherer, this is in the gatherer controller script
    }

    private void UpdateGoldText()
    {
        goldText.text = "Gold: " + currentGold.ToString();
    }
}