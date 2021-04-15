using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    public Transform player;

    private BoxCollider2D boxCollider2d;

    public bool isClimbing = false;

    private void Start()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isClimbing)
        {
            player.position = new Vector3(transform.position.x,player.position.y,player.position.z);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            if (Input.GetKeyDown("f"))
            {
                Debug.Log("press f");
                isClimbing = !isClimbing;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isClimbing = false;
        }
    }
}
