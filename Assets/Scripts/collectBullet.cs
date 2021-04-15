using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectBullet : MonoBehaviour
{
    public int bulletsToAdd = 10;
    public int bulletID = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BulletsController.instance.bullets[bulletID - 1].currentBullets = BulletsController.instance.bullets[bulletID - 1].currentBullets + bulletsToAdd;

            if (BulletsController.instance.bullets[bulletID - 1].currentBullets > BulletsController.instance.bullets[bulletID - 1].maxBullets)
            {
                BulletsController.instance.bullets[bulletID - 1].currentBullets = BulletsController.instance.bullets[bulletID - 1].maxBullets;
            }

            this.gameObject.SetActive(false);
        }
    }
}
