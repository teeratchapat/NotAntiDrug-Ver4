using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public int scoreToAdd = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().PlaySfx(13);
            FindObjectOfType<scoreManager>().addScore(scoreToAdd);
            Destroy(gameObject);
        }
    }

}
