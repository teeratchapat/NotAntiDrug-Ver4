using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ScoreBoard : MonoBehaviour
{
    public int levelToDisplay = 1;

    public string filePath;

    public Text displayLevel_Text;

    public Text[] rankText;
    public Text[] nameText;
    public Text[] scoreText;

    public List<HighScore> highScoreSaveList_level01;
    public List<HighScore> highScoreSaveList_level02;
    public List<HighScore> highScoreSaveList_level03;



    private void Start()
    {
        levelToDisplay = 1;

        if (File.Exists(Application.dataPath + filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            //FileStream fileStream = File.Open(Application.persistentDataPath + "/data.text", FileMode.Open);
            FileStream fileStream = File.Open(Application.dataPath + filePath, FileMode.Open);

            GameDataSave gameDataSave = binaryFormatter.Deserialize(fileStream) as GameDataSave;

            highScoreSaveList_level01 = gameDataSave.highScoreSaveList_level01;
            highScoreSaveList_level02 = gameDataSave.highScoreSaveList_level02;
            highScoreSaveList_level03 = gameDataSave.highScoreSaveList_level03;

            fileStream.Close();
        }
        else
        {
            Debug.Log("GameDataSave not found data");
        }

        DisplayLevel();
    }

    public void DisplayNextLevel()
    {
        levelToDisplay += 1;
        if (levelToDisplay > 3)
        {
            levelToDisplay = 1;
        }
        DisplayLevel();
    }

    public void DisplayPreviousLevel()
    {
        levelToDisplay -= 1;
        if (levelToDisplay < 1)
        {
            levelToDisplay = 3;
        }
        DisplayLevel();
    }

    public void DisplayLevel()
    {

        if (levelToDisplay == 1)
        {
            if (highScoreSaveList_level01.Count != 0)
            {
                for (int i = 0; i < highScoreSaveList_level01.Count; i++)
                {
                    rankText[i].text = (i + 1).ToString();
                    nameText[i].text = highScoreSaveList_level01[i].playerName;
                    scoreText[i].text = highScoreSaveList_level01[i].score.ToString();
                }
            }

        }
        else if (levelToDisplay == 2)
        {
            if (highScoreSaveList_level02.Count != 0)
            {
                for (int i = 0; i < highScoreSaveList_level02.Count; i++)
                {
                    rankText[i].text = (i + 1).ToString();
                    nameText[i].text = highScoreSaveList_level02[i].playerName;
                    scoreText[i].text = highScoreSaveList_level02[i].score.ToString();
                }
            }

        }
        else if (levelToDisplay == 3)
        {
            if (highScoreSaveList_level03.Count != 0)
            {
                for (int i = 0; i < highScoreSaveList_level03.Count; i++)
                {
                    rankText[i].text = (i + 1).ToString();
                    nameText[i].text = highScoreSaveList_level03[i].playerName;
                    scoreText[i].text = highScoreSaveList_level03[i].score.ToString();
                }
            }
        }

        displayLevel_Text.text = levelToDisplay.ToString();
    }
}
