using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    //[SerializeField] public int maxHealth = 100;
    public int maxHealth;
    private int currHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartingHealth(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Gatherer:
                maxHealth = 20;
                break;
            case UnitType.Range:
                maxHealth = 30;
                break;
            case UnitType.Melee:
                maxHealth = 50;
                break;
            case UnitType.Tank:
                maxHealth = 75;
                break;
            default:
                maxHealth = 10;
                break;
        }

        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(int dmgAmount)
    {
        currHealth -= dmgAmount;
        
        //If unit dies, destroy the gameObject
        if (currHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public enum UnitType
    {
        Gatherer,
        Melee,
        Range,
        Tank
    }
}
