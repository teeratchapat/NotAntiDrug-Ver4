using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    public string scene_01;
    public string scene_02;
    public string scene_03;

    public Canvas mainMenuCanvas;
    public Canvas levelSelectCanvas;
    public Canvas scoreBoardCanvas;
    public Canvas creditCanvas;

    public Button selectLevel_01_Button;
    public Button selectLevel_02_Button;
    public Button selectLevel_03_Button;

    public string filePath;

    public int unlockToLevel = 1;

    public void Start()
    {
        if (File.Exists(Application.dataPath + filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            //FileStream fileStream = File.Open(Application.persistentDataPath + "/data.text", FileMode.Open);
            FileStream fileStream = File.Open(Application.dataPath + filePath, FileMode.Open);

            GameDataSave gameDataSave = binaryFormatter.Deserialize(fileStream) as GameDataSave;

            fileStream.Close();

            unlockToLevel = gameDataSave.unlockToLevel;
        }
        else
        {
            Debug.Log("LoadHighScore not found data");
        }
    }

    public void OpenPlayMenu()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        levelSelectCanvas.gameObject.SetActive(true);

        selectLevel_01_Button.interactable = false;
        selectLevel_02_Button.interactable = false;
        selectLevel_03_Button.interactable = false;

        if (unlockToLevel == 1)
        {
            selectLevel_01_Button.interactable = true;
        }
        else if(unlockToLevel == 2)
        {
            selectLevel_01_Button.interactable = true;
            selectLevel_02_Button.interactable = true;
        }
        else if(unlockToLevel == 3)
        {
            selectLevel_01_Button.interactable = true;
            selectLevel_02_Button.interactable = true;
            selectLevel_03_Button.interactable = true;
        }
        else
        {
            selectLevel_01_Button.interactable = true;
            selectLevel_02_Button.interactable = true;
            selectLevel_03_Button.interactable = true;
        }
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
        scoreManager.currentScore = 0;
        SceneManager.LoadScene(scene_01);
    }

    public void StartLevel_02()
    {
        scoreManager.currentScore = 0;
        SceneManager.LoadScene(scene_02);
    }

    public void StartLevel_03()
    {
        scoreManager.currentScore = 0;
        SceneManager.LoadScene(scene_03);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }
}
