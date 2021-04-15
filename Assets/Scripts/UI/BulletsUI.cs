using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsUI : MonoBehaviour
{
    public static BulletsUI instance;

    public Image bulletsImage;
    public Text curruntBulletText, maxBulletText, nameBulletText;

    public Sprite bulletsSprite;

    public int i;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        i = PlayerMovement.instance.currentBulletsID-1;

        Debug.Log(i);
        //***Update UI***
        //UIsprites
        switch (PlayerMovement.instance.currentBulletsID)
        {
            case 1:
                bulletsImage.sprite = BulletsController.instance.sprites[0];
                break;
            case 2:
                bulletsImage.sprite = BulletsController.instance.sprites[1];
                break;
            case 3:
                bulletsImage.sprite = BulletsController.instance.sprites[2];
                break;
            case 4:
                bulletsImage.sprite = BulletsController.instance.sprites[3];
                break;
            case 5:
                bulletsImage.sprite = BulletsController.instance.sprites[4];
                break;
        }

        //UITextBullets
        curruntBulletText.text = BulletsController.instance.bullets[i].currentBullets.ToString();
        maxBulletText.text = BulletsController.instance.bullets[i].maxBullets.ToString();
        nameBulletText.text = BulletsController.instance.bullets[i].bulletsName.ToString();

    }
}
