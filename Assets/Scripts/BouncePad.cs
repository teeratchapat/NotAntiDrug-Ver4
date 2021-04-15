using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    //public Animator anim;

    public float bounceForce;
    void Start()
    {
        //anim.GetComponent<Animator>();
        bounceForce = 25f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement.instance.playerRigidbody2d.velocity = new Vector2(PlayerMovement.instance.playerRigidbody2d.velocity.x, bounceForce);
            Debug.Log("BouncePad detected player");
        }
    }
}
