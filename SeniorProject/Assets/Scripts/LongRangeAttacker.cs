using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttacker : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int cost;
    [SerializeField] private int speed;
    [SerializeField] private int strength;
    [SerializeField] private int attacksPerSecond;
    [SerializeField] private int attackRange;
    [SerializeField] private int projectileSpeed;
    [SerializeField] private float projectileYOffset;
    [SerializeField] public LayerMask targetLayer;
    private bool isAttacking;
    private float detectRange = 50.0f;   //Range of which the unit can detect

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        findClosestTarget();
        Debug.Log($"{target}");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) //Check for target
        {
            //Target distance from unit and target
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance <= attackRange) //Attack if within range
            {
                Debug.Log($"attacking {target}");
                attack();
            } else
            {
                Debug.Log("target out of range");
            }
        }
        else
        {
            Debug.Log("target is null");
        }
    }

    public void attack()
    {
        if (!isAttacking)
        {
            var position = transform.position;
            position.y += projectileYOffset;
            Vector3 angle = position - target.position;
            angle.z = Mathf.Rad2Deg * (Mathf.Atan2(angle.y, angle.x)) + 90;
            GameObject launched_projectile = Instantiate(projectile, position, Quaternion.Euler(0, 0, angle.z));
            launched_projectile.GetComponent<ProjectileController>().Launch(target, projectileSpeed);
            isAttacking = true;
            StartCoroutine("attackCooldown");
        }
    }

    private void findClosestTarget() // sets target to closest target
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detectRange, targetLayer);

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider2D nearest in targets)
        {
            float distance = Vector2.Distance(transform.position, nearest.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = nearest.transform;
            }
        }
        target = closestTarget;
    }

    private IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(1 / attacksPerSecond);
        isAttacking = false;
    }
}