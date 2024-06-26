﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsUI : MonoBehaviour
{
    public static BulletsUI instance;

    public Image bulletsImage;
    public Text curruntBulletText, maxBulletText, nameBulletText;

    public int i;

    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        updateBulletUI();
    }

    public void updateBulletUI()
    {
        i = PlayerMovement.instance.currentBulletsID - 1;

        bulletsImage.sprite = BulletsController.instance.sprites[i];

        //UITextBullets
        curruntBulletText.text = BulletsController.instance.bullets[i].currentBullets.ToString();
        maxBulletText.text = BulletsController.instance.bullets[i].maxBullets.ToString();
        nameBulletText.text = BulletsController.instance.bullets[i].bulletsName.ToString();
    }
}