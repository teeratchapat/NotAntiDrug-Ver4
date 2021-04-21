using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingVerticalPlatformer : MonoBehaviour
{
    public float speed = 3f;
    public float Ymin;
    public float Ymax;
    //public float waittime;
    public bool moveUp = true;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y >= Ymax)
        {
            moveUp = false;
        }
        else if (transform.position.y <= Ymin)
        {
            moveUp = true;
        }

        if (moveUp)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

    }

    /*void OnCollisionEnter2D(Collision2D other)
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
    }*/
}
