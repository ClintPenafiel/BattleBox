using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rigBod2D;

    private float projectileSpeed;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigBod2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigBod2D.velocity = moveDirection * projectileSpeed;
    }

    public void Launch(Transform target, float speed, float yOffset, float maxDistance)
    {
        var position = target.position;
        position.y += yOffset;
        moveDirection = (position - transform.position).normalized;
        projectileSpeed = speed;
        Destroy(gameObject, 1 / speed * maxDistance);
    }

}