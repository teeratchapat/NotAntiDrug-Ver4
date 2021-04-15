using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    private Bullets.BulletsType bulletsType;

    [SerializeField]
    private float bulletSpeed = 10f;

    [SerializeField]
    private Rigidbody2D bulletRigidbody;

    public int bulletDamage = 2;
    
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    public void throwDirection(Vector2 direction)
    {
        bulletRigidbody.velocity = direction * bulletSpeed;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if (bulletsType == Bullets.BulletsType.antiAll)
            {
                OnHitEnemy(collision);
            }
            else if (bulletsType == Bullets.BulletsType.antiDepressants && collision.GetComponent<Enemy>().scriptableEnemy.effectType == ScriptableEnemy.EffectType.depressants)
            {
                OnHitEnemy(collision);
            }
            else if (bulletsType == Bullets.BulletsType.antiHallucinogens && collision.GetComponent<Enemy>().scriptableEnemy.effectType == ScriptableEnemy.EffectType.hallucinogens)
            {
                OnHitEnemy(collision);
            }
            else if (bulletsType == Bullets.BulletsType.antiStimulants && collision.GetComponent<Enemy>().scriptableEnemy.effectType == ScriptableEnemy.EffectType.stimulants)
            {
                OnHitEnemy(collision);
            }
            else if (bulletsType == Bullets.BulletsType.antiMultipleEffect && collision.GetComponent<Enemy>().scriptableEnemy.effectType == ScriptableEnemy.EffectType.multipleEffect)
            {
                OnHitEnemy(collision);
            }

        }
        
        if(collision.tag == "Platform")
        {
            Destroy(gameObject, 0.0125f);
        }
    }

    private void OnHitEnemy(Collider2D collision)
    {
        Destroy(gameObject, 0.0125f);
        collision.GetComponent<Enemy>().damageEnemy(bulletDamage);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 0.0125f);
    }
}
