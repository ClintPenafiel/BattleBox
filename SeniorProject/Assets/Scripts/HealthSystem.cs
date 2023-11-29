using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    //public int maxHealth;
    private int currHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartingHealth(unitType unitType)
    {
        switch (unitType)
        {
            case unitType.Gatherer:
                maxHealth = 20;
                break;
            case unitType.Range:
                maxHealth = 30;
                break;
            case unitType.Melee:
                maxHealth = 50;
                break;
            case unitType.Tank:
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // check if layers are different (player/enemy) and if collider is a projectile
        if (other.gameObject.layer != gameObject.layer && other.gameObject.CompareTag("Projectile"))
        {
            // destroy projectile
            Destroy(other.gameObject);
            // TODO deplete health here
            
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public enum unitType
    {
        Gatherer,
        Melee,
        Range,
        Tank
    }
}
