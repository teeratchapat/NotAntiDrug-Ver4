using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image heart_1, heart_2, heart_3;

    public Sprite heartFull, heartEmpty;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Player.instance.currentHP)
        {
            case 3:
                heart_1.sprite = heartFull;
                heart_2.sprite = heartFull;
                heart_3.sprite = heartFull;
                break;

            case 2:
                heart_1.sprite = heartFull;
                heart_2.sprite = heartFull;
                heart_3.sprite = heartEmpty;
                break;

            case 1:
                heart_1.sprite = heartFull;
                heart_2.sprite = heartEmpty;
                heart_3.sprite = heartEmpty;
                break;

            case 0:
                heart_1.sprite = heartEmpty;
                heart_2.sprite = heartEmpty;
                heart_3.sprite = heartEmpty;
                break;

        }
    }
}
