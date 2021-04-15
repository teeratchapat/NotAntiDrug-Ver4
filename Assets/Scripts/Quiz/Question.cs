using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public string question;
    public string choice1;
    public string choice2;
    public string choice3;
    public string choice4;
    public int key;                 //  1-4
    public int scoreReward;

    public Question(string question,string choice1,string choice2,string choice3,string choice4,int key,int scoreReward)
    {
        this.question = question;
        this.choice1 = choice1;
        this.choice2 = choice2;
        this.choice3 = choice3;
        this.choice4 = choice4;
        this.key = key;
        this.scoreReward = scoreReward;
    }
}
