using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite checkPointOnSprite, checkPointOffSprite;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().PlaySfx(15);
            for(int i = 0; i < BulletsController.instance.bullets.Count; i++)
            {
                BulletsController.instance.bullets[i].currentBullets = BulletsController.instance.bullets[i].maxBullets;
            }

            Player.currentHP = Player.instance.maxHP;
            //BulletsController.instance.bullets.Count;
            LevelManager.instance.DeactiveCheckPoint();
            spriteRenderer.sprite = checkPointOnSprite;
            LevelManager.instance.SetSpawnPoint(transform.position);
            //PlayerController.instance.currentHP = PlayerController.instance.maxHP;
            FindObjectOfType<HPbarUI>().updateHpUI();
        }
    }

    public void ResetCheckPoint()
    {
        spriteRenderer.sprite = checkPointOffSprite;
    }
}
