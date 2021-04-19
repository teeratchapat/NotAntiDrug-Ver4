using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScore
{
    public string playerName;
    public int score;

    public HighScore (string playerName,int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}
