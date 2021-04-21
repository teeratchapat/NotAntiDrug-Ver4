using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullets
{
    //public static Bullets instance;
    public enum BulletsType
    {
        antiAll,
        antiDepressants,
        antiHallucinogens,
        antiStimulants,
        antiMultipleEffect
    };

    public string bulletsName;
    public BulletsType bulletsType;
    public int maxBullets;
    public int currentBullets;
    public int damagePower;
    public string description;
    public Sprite sprite;
    public GameObject bulletPrefab;

    public Bullets(BulletsType newBulletsType, string newBulletsName, int newMaxBullets, int newDamagePower, string newDescription, Sprite newSprite, GameObject newBulletPrefab)
    {
        bulletsType = newBulletsType;
        bulletsName = newBulletsName;
        maxBullets = newMaxBullets;
        currentBullets = newMaxBullets;
        damagePower = newDamagePower;
        description = newDescription;
        sprite = newSprite;
        bulletPrefab = newBulletPrefab;
    }
   
}
