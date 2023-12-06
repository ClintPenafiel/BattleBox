using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private Rigidbody2D rigBod2D;
    private Transform myTransform;
    private bool facingLeft;
    // Start is called before the first frame update
    void Start()
    {
        facingLeft = true;
        myTransform = GetComponent<Transform>();
        rigBod2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if((facingLeft && rigBod2D.velocity.x > 0) || (!facingLeft && rigBod2D.velocity.x < 0)) {
            FlipImage();
        }
    }

    private void FlipImage()
    {
        facingLeft = !facingLeft;
        var localScale = myTransform.localScale;
        localScale.x *= -1;
        myTransform.localScale = localScale;
    }
}
