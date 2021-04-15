using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public enum Speaker
    {
        player,
        doctor,
        Boss1,
        Boss2,
        Boss3,
    }

    public static string[] inGameNameSpeaker = {
        "น็อต",
        "ด็อกเตอร์",
        "บอส1",
        "บอส2",
        "บอส3" 
    };
    
    public Speaker speaker;
    
    [TextArea(3,5)]
    public string quote;

    public Sprite insertImage;
}