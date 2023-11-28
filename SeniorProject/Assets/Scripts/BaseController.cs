using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{
    public static BaseController Instance { get; private set; }

    public GoldManager GoldManager { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Destroy(gameObject);
            return;
        }

        GoldManager = FindObjectOfType<GoldManager>();
    }


    private void Update()
    {
        
    }

    public int GetGold()
    {
        return GoldManager != null ? GoldManager.currentGold : 0;
    }
    
    public int GetEnemyGold()
    {
        return GoldManager != null ? GoldManager.currentEnemyGold : 0;
    }

    public void AddGold(int num)
    {
        if (GoldManager != null)
        {
            GoldManager.DepositGold(num);
        }
    }
    
    public void AddEnemyGold(int num)
    {
        if (GoldManager != null)
        {
            GoldManager.DepositEnemyGold(num);
        }
    }

    public void SubtractGold(int num)
    {
        if (GoldManager != null)
        {
            GoldManager.DepositGold(-num);
        }
    }


}
