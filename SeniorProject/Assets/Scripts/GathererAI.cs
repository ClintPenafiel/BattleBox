using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GathererAI : MonoBehaviour
{
    [SerializeField] private GathererController gathererController;
    public float speed = 5f;            //Movement speed of the unit
    //public float collectRange = 1.0f;   //Range of which the unit gathers gold
    public float stopDistance = 0.5f;   //Distance to stop when collecting gold
    public float detectRange = 50.0f;   //Range of which the unit can detect gold
    public LayerMask goldResourceLayer; //Layer containing gold resources
    public Transform target;//Reference to currently target
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
        if (target != null) //Check for target
        {
            //Target distance from unit and target gold
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance <= stopDistance) //Check if within range of target
            {
                rigBod2D.velocity = Vector2.zero; //Stop moving when collecting/depositing gold
                if (target == gathererController.baseTransform())
                {
                    StartCoroutine(gathererController.DepositGold());
                    if (!gathererController.depositState()) // switch target once deposit is done
                    {
                        findClosestGoldResource();
                    }
                }
                else
                {
                    findClosestGoldResource();
                    StartCoroutine(gathererController.GatherGold(target));
                    if (!gathererController.gatherState()) // switch target once gather is done
                    {
                        target = gathererController.baseTransform();
                    }
                }
            }
            else
            {
                //Move towards target
                Vector2 moveDirection = (target.position - transform.position).normalized;
                rigBod2D.velocity = moveDirection * speed;
            }
        }
        else
        {
            rigBod2D.velocity = Vector2.zero; //Stop moving if there is no target
        }
    }

    private void findClosestGoldResource() // sets target to closest gold resource
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

        target = closestGold;
    }
}
