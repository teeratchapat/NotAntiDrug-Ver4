using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbarUI : MonoBehaviour
{
    public Transform Hpbarholder;

    public Text currentHpText;
    public Text maxHpText;

    public float hpPercentage = 1;

    public void updateHpUI()
    {
        hpPercentage = Player.instance.currentHP / Player.instance.maxHP;
        Hpbarholder.localScale = new Vector3(hpPercentage, 1, 1);

        currentHpText.text = Player.instance.currentHP.ToString();
        maxHpText.text = Player.instance.maxHP.ToString();
    }
}
