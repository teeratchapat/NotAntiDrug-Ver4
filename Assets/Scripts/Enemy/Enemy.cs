using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //public static Enemy instance;

    public ScriptableEnemy scriptableEnemy;

    public Transform hpBarHolder;

    //public GameObject itemDrop;

    public float enemyHp;
    public float hpPercentage = 1;

    private void Awake()
    {
        //instance = this;
    }
    private void Start()
    {
        enemyHp = scriptableEnemy.enemyMaxHp;
        UpdateHpUI();
    }
    private void onCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player hit");
            Player.instance.dealDamage(scriptableEnemy.enemyDamage);
        }
    }

    public void damageEnemy(int dmg)
    {
        enemyHp -= dmg;

        UpdateHpUI();

        if (enemyHp <= 0)
        {
            Destroy(gameObject);

            FindObjectOfType<scoreManager>().addScore(scriptableEnemy.scoreReward);
            /*
            int rand = Random.Range(1, 100);

            if (rand <= 50)
            {
                Instantiate(itemDrop, transform.position, itemDrop.transform.rotation);
            }
            */
        }
    }

    public void UpdateHpUI()
    {
        hpPercentage = enemyHp / scriptableEnemy.enemyMaxHp;
        hpBarHolder.localScale = new Vector3(hpPercentage, 1, 1);
    }
}
