using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolBehavior : MonoBehaviour
{
    private BoxCollider2D boxCollider2d;
    private Rigidbody2D rigidBody2d;

    //[SerializeField] LayerMask platformlayerMask; 

    private Vector3 groundCheckerOriginLeft;
    private Vector3 groundCheckerOriginRight;
    private Vector3 groundCheckerDirection;

    private bool isFacingLeft = true;

    public float speed = 1f;
    public float offset = 0.1f;
    public float size = 0.4f;

    public LayerMask whatIsGround;

    void Start()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
        rigidBody2d = GetComponent<Rigidbody2D>();
        offset = size / 2f;
    }

    void Update()
    {
        //set origin & Direction of groundChecker
        groundCheckerOriginLeft = boxCollider2d.bounds.center + new Vector3(-(boxCollider2d.bounds.extents.x + offset), -(boxCollider2d.bounds.extents.y - offset), 0);
        groundCheckerOriginRight = boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x + offset, -(boxCollider2d.bounds.extents.y - offset), 0);
        groundCheckerDirection = Vector2.down * (size);

        //set origin & Direction of otherEnemyChecker
        



        if (!Physics2D.Raycast(groundCheckerOriginLeft, Vector2.down, size))
        {
            isFacingLeft = false;
            //Debug.Log("Raycast left not hit platform");
        }

        if (!Physics2D.Raycast(groundCheckerOriginRight, Vector2.down, size))
        {
            isFacingLeft = true;
            //Debug.Log("Raycast right not hit platform");
        }
        
        //GroundChecker
        Debug.DrawRay(groundCheckerOriginLeft, groundCheckerDirection, Color.red);
        Debug.DrawRay(groundCheckerOriginRight, groundCheckerDirection,Color.red);
        //WallChecker
    }

    private void FixedUpdate()
    {
            if (isFacingLeft)
            {
                rigidBody2d.velocity = new Vector2(-speed,rigidBody2d.velocity.y);
            }
            else
            {
                rigidBody2d.velocity = new Vector2(speed, rigidBody2d.velocity.y);
            }
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }
}