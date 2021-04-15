using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movingHorizonPlatform : MonoBehaviour
{
    public float speed = 3f;
    public float Xmin;
    public float Xmax;
    public bool moveRight = true;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if(transform.position.x >= Xmax)
        {
            moveRight = false;
        }else if(transform.position.x <= Xmin)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.Translate(Vector2.right * speed*Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed*Time.deltaTime);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = transform;
        }
    }


    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }
}
