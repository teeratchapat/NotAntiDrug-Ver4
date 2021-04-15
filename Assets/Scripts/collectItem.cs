using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectItem : MonoBehaviour
{
    //public GameObject scoreManager;
    public int itemScore = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<scoreManager>().addScore(itemScore);
            Destroy(gameObject);
        }
    }
}
