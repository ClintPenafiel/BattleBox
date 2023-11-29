using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniHealthSystem : MonoBehaviour
{
    public GameObject gathererPrefab;
    public GameObject meleePrefab;
    public GameObject rangePrefab;
    public GameObject tankPrefab;

    /*[SerializeField] public int gathererHealth = 40;
    [SerializeField] public int meleeHealth = 75;
    [SerializeField] public int rangeHealth = 50;
    [SerializeField] public int tankHealth = 100;*/

    private int currHealth;

    void Start()
    {
        InitializeHealth();
    }

    private void InitializeHealth()
    {
        if (gameObject.CompareTag("Gatherer"))
        {
            currHealth = 40;
        }
        else if (gameObject.CompareTag("Melee"))
        {
            currHealth = 75;
        }
        else if (gameObject.CompareTag("Range"))
        {
            currHealth = 50;
        }
        else if (gameObject.CompareTag("Tank"))
        {
            currHealth = 100;
        }
        else
        {
            Debug.LogWarning("Unknown unit type. Setting default health to 50.");
            currHealth = 50;
        }
    }

    private void TakeDmg(int dmgAmount)
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
            TakeDmg(10);
            
        }
    }
    
    void Die()
    {
        Destroy(gameObject);
    }
    
}
