using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GathererState
{
    Idle,
    Gathering,
    Depositing
}

public class GathererAI : MonoBehaviour
{
    [SerializeField] private GathererController gathererController;
    public float speed = 5f;
    public float stopDistance = 0.5f;
    public float detectRange = 50.0f;
    public LayerMask goldResourceLayer;
    public Transform target;
    private Rigidbody2D rigBod2D;
    public Animator animator;
    private GathererState state = GathererState.Idle;

    void Start()
    {
        rigBod2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gathererController = GetComponent<GathererController>();
        findClosestGoldResource();
    }

    void Update()
    {
        if (state == GathererState.Idle)
        {
            findClosestGoldResource();
            state = GathererState.Gathering;
        }
        else if (state == GathererState.Gathering)
        {
            float distance = target != null ? Vector2.Distance(transform.position, target.position) : Mathf.Infinity;

            if (distance <= stopDistance)
            {
                rigBod2D.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);

            if (!gathererController.gatherState() && gathererController.GetGoldCarried() < gathererController.GetCarryCapacity())
            {
                StartCoroutine(gathererController.GatherGold(target));
                animator.SetBool("isMining", true);
            }

            if (gathererController.GetGoldCarried() >= gathererController.GetCarryCapacity())
            {
                target = gathererController.baseTransform();
                state = GathererState.Depositing;
            }
            }
            else
            {
                Vector2 moveDirection = target != null ? (target.position - transform.position).normalized : Vector2.zero;

                rigBod2D.velocity = moveDirection * speed;
                animator.SetBool("isMoving", true);
                animator.SetBool("isMining", false);
            }
        }
        else if (state == GathererState.Depositing)
        {
            float distance = target != null ? Vector2.Distance(transform.position, target.position) : Mathf.Infinity;

            if (distance <= stopDistance)
            {
                rigBod2D.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);

                if (!gathererController.depositState())
                {
                    StartCoroutine(gathererController.DepositGold());
                    animator.SetBool("isMining", false);
                }

                if (gathererController.depositState())
                {
                    state = GathererState.Idle;
                }
            }
            else
            {
                Vector2 moveDirection = target != null ? (target.position - transform.position).normalized : Vector2.zero;

                rigBod2D.velocity = moveDirection * speed;
                animator.SetBool("isMoving", true);
                animator.SetBool("isMining", false);
            }
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
