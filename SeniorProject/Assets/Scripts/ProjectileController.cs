using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rigBod2D;

    private int projectileSpeed;

    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        GameObject o;
        rigBod2D = (o = gameObject).GetComponent<Rigidbody2D>();
        Destroy(o, 3);
    }

    // Update is called once per frame
    void Update()
    {
        rigBod2D.velocity = moveDirection * projectileSpeed;
    }

    public void Launch(Transform target, int speed)
    {
        // FromToRotation(Vector3.zero, transform.position)
        moveDirection = (target.position - transform.position).normalized;
        projectileSpeed = speed;
    }

}
