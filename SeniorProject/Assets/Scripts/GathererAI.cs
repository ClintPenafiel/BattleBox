using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GathererAI : MonoBehaviour
{
    [SerializeField] private GathererController gathererController;
    public float speed = 5f;            //Movement speed of the unit
    public float collectRange = 1.0f;   //Range of which the unit gathers gold
    public float detectRange = 50.0f;   //Range of which the unit can detect gold
    public LayerMask goldResourceLayer; //Layer containing gold resources
    public Transform targetGoldResource;//Reference to currently targeted gold resource
    private Rigidbody2D rigBod2D;       //Component for moving
    
    // Start is called before the first frame update
    void Start()
    {
        rigBod2D = GetComponent<Rigidbody2D>();
        findClosestGoldResource(); 
        //If user does not tell unit to go to a specific resource, the unit will
        //go to the closest one
    }

    // Update is called once per frame
    void Update()
    {
        if (targetGoldResource != null) //Check for user targeted gold
        {
            //Target distance from unit and target gold
            float distance = Vector2.Distance(transform.position, targetGoldResource.position);

            if (distance <= collectRange) //Collect gold if within range
            {
                collectGold();
                findClosestGoldResource(); //Find a new gold resource
            }
            else
            {
                //Move towards target gold resource
                Vector2 moveDirection = (targetGoldResource.position - transform.position).normalized;
                rigBod2D.velocity = moveDirection * speed;
            }
        }
        else
        {
            rigBod2D.velocity = Vector2.zero; //Stop moving if there is no target
        }
    }

    private void collectGold()
    {
        StartCoroutine(gathererController.GatherGold(targetGoldResource));
        // TODO move the gatherer to the base and deposit gold
        // StartCoroutine(gathererController.DepositGold());
    }

    private void findClosestGoldResource()
    {
        Collider2D[] goldResources = Physics2D.OverlapCircleAll(transform.position, detectRange, goldResourceLayer);

        float closestDistance = Mathf.Infinity;
        Transform closestGold = null;

        foreach (Collider2D goldResource in goldResources)
        {
            float distance = Vector2.Distance(transform.position, goldResource.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGold = goldResource.transform;
            }
        }

        targetGoldResource = closestGold;
    }
}
