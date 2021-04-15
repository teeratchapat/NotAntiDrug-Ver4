using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy",menuName ="Enemy",order =1)]
public class ScriptableEnemy : ScriptableObject
{
    public enum EnemyName
    {
        none,
        Alcohol,
        Glue,
        Opium,
        Amphetamine,
        Cocaine,
        Ecstasy,
        Mitragynine,
        MagicMushroom
    }

    public enum EffectType
    {
        depressants,
        hallucinogens,
        stimulants,
        multipleEffect
    }

    public EnemyName enemyName;
    public EffectType effectType;
    public float enemyMaxHp;
    public float enemyDamage;
    public int scoreReward;
}