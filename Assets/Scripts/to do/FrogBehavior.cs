using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBehavior : MonoBehaviour
{
    private Rigidbody2D frogRigidbody2d;
    public float xForce = 2;
    public float jumpForce = 20;
    public float rand = 1;
    public float jumpRate = 1;
    public float nextJump = 0;
    public Transform playerTransform;
    public Vector2 jumpVector;
    // Start is called before the first frame update
    void Start()
    {
        frogRigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            jumpVector = new Vector2(-xForce, jumpForce);
        }
        else
        {
            jumpVector = new Vector2(xForce,jumpForce);
        }

        if (nextJump <= Time.time)
        {
            rand = Random.Range(1, 10);
            if (rand == 1)
            {
                frogRigidbody2d.velocity = jumpVector;
                nextJump = jumpRate + Time.time;
            }
        }

        
        
        
    }
}
