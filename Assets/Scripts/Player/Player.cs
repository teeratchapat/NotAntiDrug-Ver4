using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private SpriteRenderer spriteRendererPlayer;
    public Animator anim;

    public float maxHP;
    public float currentHP;

    public float invincibleLength;
    public float invincibleCounter;

    public float flashingDuration;
    public float flashingCounter;
    public bool isFlashed = false;

    public Color NormalColor;
    public Color FlashColor;

    public int scoreToReduce = 100;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        maxHP = 100;
        currentHP = maxHP;

        spriteRendererPlayer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        FindObjectOfType<HPbarUI>().updateHpUI();

        NormalColor = new Color(spriteRendererPlayer.color.r, spriteRendererPlayer.color.g, spriteRendererPlayer.color.b, 1f);
        FlashColor = new Color(spriteRendererPlayer.color.r, spriteRendererPlayer.color.g, spriteRendererPlayer.color.b, 0.5f);
    }

    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            flashingCounter -= Time.deltaTime;

            if(flashingCounter <= 0)
            {
                flashingCounter = flashingDuration;

                if (isFlashed == true) {
                    spriteRendererPlayer.color = NormalColor;
                    isFlashed = false;
                }
                else
                {
                    spriteRendererPlayer.color = FlashColor;
                    isFlashed = true;
                }
            }

            if(invincibleCounter <= 0)
            {
                spriteRendererPlayer.color = NormalColor;
            }
        }
    }

    public void dealDamage(float damage)
    {

        if (invincibleCounter <= 0)
        {
            currentHP = currentHP - damage;
            FindObjectOfType<HPbarUI>().updateHpUI();

            if (currentHP <= 0)
            {
                FindObjectOfType<scoreManager>().reduceScore(scoreToReduce);
                //death
                //Debug.Log("death");
                anim.SetTrigger("deadTrigger");

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                //do knockback
                PlayerMovement.instance.knockback();

                //invincible state
                invincibleCounter = invincibleLength;
                //
                
            }
        }
    }
    
    public void healDamage(float heal)
    {
        currentHP += heal;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        FindObjectOfType<HPbarUI>().updateHpUI();
    }
}
