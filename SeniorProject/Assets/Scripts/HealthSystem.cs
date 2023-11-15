using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    private int currHealth;
    
    // Start is called before the first frame update
    void Start()
    {
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
        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
