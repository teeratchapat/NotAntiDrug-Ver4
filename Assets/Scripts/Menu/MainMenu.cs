using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string scene_01;
    public string scene_02;
    public string scene_03;

    public Canvas mainMenuCanvas;
    public Canvas levelSelectCanvas;
    public Canvas scoreBoardCanvas;
    public Canvas creditCanvas;

    public void OpenPlayMenu()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        levelSelectCanvas.gameObject.SetActive(true);
    }

    public void OpenScoreBoard()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        scoreBoardCanvas.gameObject.SetActive(true);
    }
    
    public void OpenCredit()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        creditCanvas.gameObject.SetActive(true);
    }

    public void BackToTitle()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        levelSelectCanvas.gameObject.SetActive(false);
        scoreBoardCanvas.gameObject.SetActive(false);
        creditCanvas.gameObject.SetActive(false);
    }

    public void StartLevel_01()
    {
        SceneManager.LoadScene(scene_01);
    }

    public void StartLevel_02()
    {
        SceneManager.LoadScene(scene_02);
    }

    public void StartLevel_03()
    {
        SceneManager.LoadScene(scene_03);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }
}
