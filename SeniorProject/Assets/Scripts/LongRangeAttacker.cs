using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttacker : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int speed;
    [SerializeField] private int strength;
    [SerializeField] private float attacksPerSecond;
    [SerializeField] private float attackRange;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileYOffset;
    [SerializeField] public LayerMask targetLayer;
    [SerializeField] private float detectRange = 50.0f;   //Range of which the unit can detect
    private Rigidbody2D rigBod2D;
    private bool isAttacking;
    
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rigBod2D = GetComponent<Rigidbody2D>();
        FindClosestTarget();
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
                rigBod2D.velocity = Vector2.zero;
                Attack();
            } else //if not within attack range, move towards target
            {
                Vector2 moveDirection = target != null ? (target.position - transform.position).normalized : Vector2.zero;
                rigBod2D.velocity = moveDirection * speed;
            }
        }
        else
        {
            FindClosestTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // check if layers are different (player/enemy) and if collider is a projectile
        if (other.gameObject.layer != gameObject.layer && other.gameObject.CompareTag("Projectile"))
        {
            GameObject o;
            // TODO deplete health here
            (o = other.gameObject).GetComponent<ProjectileController>().GetDamage(); // get damage value of projectile
            // destroy projectile
            Destroy(o);
        }
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            var position = transform.position;
            position.y += projectileYOffset;
            Vector3 angle = position - target.position;
            angle.z = Mathf.Rad2Deg * (Mathf.Atan2(angle.y, angle.x)) + 90;
            GameObject launchedProjectile = Instantiate(projectile, position, Quaternion.Euler(0, 0, angle.z));
            launchedProjectile.GetComponent<ProjectileController>().Launch(target, projectileSpeed, projectileYOffset, attackRange, strength);
            isAttacking = true;
            StartCoroutine("AttackCooldown");
        }
    }

    private void FindClosestTarget() // sets target to closest target
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detectRange, targetLayer);

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider2D nearest in targets)
        {
            if (nearest.CompareTag("Projectile")) continue; // make sure nearest collider is not a projectile
            var distance = Vector2.Distance(transform.position, nearest.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = nearest.transform;
            }
        }
        target = closestTarget;
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1 / attacksPerSecond);
        isAttacking = false;
    }
}