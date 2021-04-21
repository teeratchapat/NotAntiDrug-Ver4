using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCutscene : MonoBehaviour
{
    public GameObject[] cutscene;
    public int index;

    private void Start()
    {
        FindObjectOfType<PauseGameScript>().pauseGame();
        index = 0;
        cutscene[index].SetActive(true);
    }

    private void Update()
    {
        FindObjectOfType<PauseGameScript>().pauseGame();
        if (Input.anyKeyDown)
        {
            for(int i = 0; i < cutscene.Length; i++)
            {
                cutscene[i].SetActive(false);
            }

            index++;
            if (index < cutscene.Length)
            {
                cutscene[index].SetActive(true);
            }
            else
            {
                FindObjectOfType<PauseGameScript>().unPauseGame();
                this.gameObject.SetActive(false);
            }
        }
    }
}
