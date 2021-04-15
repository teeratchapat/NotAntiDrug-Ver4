using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour
{
    public GameObject pauseMenu;

    public bool isPause = false;

    public string mainmenuScene;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Time.timeScale = 1f;

                pauseMenu.SetActive(false);
                isPause = false;
            }
            else
            {
                Time.timeScale = 0f;

                pauseMenu.SetActive(true);
                isPause = true;
            }

        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        isPause = false;
    }

    public void ToMainmenu()
    {
        SceneManager.LoadScene(mainmenuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }
}
