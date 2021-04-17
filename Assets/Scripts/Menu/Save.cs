using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int level = 0;

    public float playerPosX;
    public float playerPosY;
    public float playerHp;

    public int[] bulletsLeft;

    public List<EnemyDataToSave> enemiesList;

    public int score;
}
