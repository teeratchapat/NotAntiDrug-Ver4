using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Bullet",menuName ="Bullet",order =1)]
public class ScriptableBullet : ScriptableObject
{
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
    public int damagePower;
    public string description;
    public Sprite sprite;
}
