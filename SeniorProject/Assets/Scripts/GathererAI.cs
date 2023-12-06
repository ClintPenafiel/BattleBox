using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private bool animate;
    private GathererState state = GathererState.Idle;
    private static readonly int IsMoving = Animator.StringToHash("isMoving"); // set IsMoving based on corresponding hash value
    private static readonly int IsMining = Animator.StringToHash("isMining"); // set IsMining based on corresponding hash value

    void Start()
    {
        rigBod2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animate = false;
        foreach (var parameter in animator.parameters)
        {
            // animate if the animator has the hash for IsMoving or IsMining
            if (parameter.nameHash == IsMoving || parameter.nameHash == IsMining)
            {
                animate = true;
            }
        }
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
                if (animate)
                {
                    animator.SetBool(IsMoving, false);
                }

            if (!gathererController.gatherState() && gathererController.GetGoldCarried() < gathererController.GetCarryCapacity())
            {
                StartCoroutine(gathererController.GatherGold(target));
                if (animate)
                {
                    animator.SetBool(IsMining, true);
                }
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
                if (animate)
                {
                    animator.SetBool(IsMoving, true);
                    animator.SetBool(IsMining, false);
                }
                
            }
        }
        else if (state == GathererState.Depositing)
        {
            float distance = target != null ? Vector2.Distance(transform.position, target.position) : Mathf.Infinity;

            if (distance <= stopDistance)
            {
                rigBod2D.velocity = Vector2.zero;
                if (animate)
                {
                    animator.SetBool(IsMoving, false);
                }
                

                if (!gathererController.depositState())
                {
                    StartCoroutine(gathererController.DepositGold());
                    if (animate)
                    {
                        animator.SetBool(IsMining, false);
                    }
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
                if (animate)
                {
                    animator.SetBool(IsMoving, true);
                    animator.SetBool(IsMining, false);
                }
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
