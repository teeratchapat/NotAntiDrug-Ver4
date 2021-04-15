using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueFloorScript : MonoBehaviour
{
    public float slowRate = 50f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement.instance.speed = PlayerMovement.instance.speed * (slowRate / 100);
            Debug.Log("trigged glue");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement.instance.speed = PlayerMovement.instance.speed * (100 / slowRate);
        }
    }
}
