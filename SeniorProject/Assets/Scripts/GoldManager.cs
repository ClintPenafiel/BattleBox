using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;
    public int startingGold = 0;
    public int currentGold = 0;
    public int startingEnemyGold = 0;
    public int currentEnemyGold = 0;
    [SerializeField] private bool showEnemyGold;
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
    currentEnemyGold = startingEnemyGold;
    goldText = GameObject.Find("GoldText").GetComponent<Text>(); 

}
//it should add all the gold that the gatherer has to the base when it reaches the base
    public void DepositGold(int gold)
    {
        currentGold += gold;
        UpdateGoldText(); 
    }
    
    public void DepositEnemyGold(int gold)
    {
        currentEnemyGold += gold;
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        String goldDisplayText = "Gold: " + currentGold;
        if (showEnemyGold)
        {
            goldDisplayText += " Enemy Gold: " + currentEnemyGold;
        }
        if (goldText == null)
        {
            goldText = GameObject.Find("GoldText").GetComponent<Text>(); 
        }
        goldText.text = goldDisplayText;
    }
}