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

    private void Start()
    {
        isPause = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                unPauseGame();
            }
            else
            {
                pauseGame();
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

    public void pauseGame()
    {
        Time.timeScale = 0f;

        pauseMenu.SetActive(true);
        isPause = true;
    }

    public void unPauseGame()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        isPause = false;
    }
}
