using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
//
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    //public int maxHealth;
    private int currHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
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
        Debug.Log($"current health {currHealth}, damage: {dmgAmount}");
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
            GameObject o;
            int damage = (o = other.gameObject).GetComponent<ProjectileController>().GetDamage(); // get damage value of projectile
            TakeDmg(damage);
            // destroy projectile
            Destroy(o);
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Base"))
        {
            Debug.Log("Player lose");
            // TODO trigger game over
        }
        if (gameObject.CompareTag("EnemyBase"))
        {
            Debug.Log("Player win");
            // TODO trigger player win
        }
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
