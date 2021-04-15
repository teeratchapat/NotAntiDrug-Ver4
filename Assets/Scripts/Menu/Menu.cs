using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.playerPosX = Player.instance.transform.position.x;
        save.playerPosY = Player.instance.transform.position.y;
        save.playerHp = Player.instance.currentHP;

        save.currentBullet_0 = BulletsController.instance.bullets[0].currentBullets;
        save.currentBullet_1 = BulletsController.instance.bullets[1].currentBullets;
        /*save.currentBullet_2 = BulletsController.instance.bullets[2].currentBullets;
        save.currentBullet_3 = BulletsController.instance.bullets[3].currentBullets;
        save.currentBullet_4 = BulletsController.instance.bullets[4].currentBullets;*/

        save.score = scoreManager.currentScore;

        Debug.Log("save : " + save.playerPosX + save.playerPosY + save.playerHp + save.currentBullet_0);

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        //FileStream fileStream = File.Create(Application.persistentDataPath + "/data.text");
        FileStream fileStream = File.Create(Application.dataPath + "/data.text");

        binaryFormatter.Serialize(fileStream, save);

        fileStream.Close();

    }

    public void LoadGame()
    {
        Debug.Log("LoadGame Calling");
        if (File.Exists(Application.dataPath + "/data.text"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            //FileStream fileStream = File.Open(Application.persistentDataPath + "/data.text", FileMode.Open);
            FileStream fileStream = File.Open(Application.dataPath + "/data.text", FileMode.Open);

            Save save = binaryFormatter.Deserialize(fileStream) as Save;

            fileStream.Close();

            Player.instance.transform.position = new Vector2(save.playerPosX, save.playerPosY);
            Player.instance.currentHP = save.playerHp;

            BulletsController.instance.bullets[0].currentBullets = save.currentBullet_0;
            BulletsController.instance.bullets[1].currentBullets = save.currentBullet_1;
            /*BulletsController.instance.bullets[2].currentBullets = save.currentBullet_2;
            BulletsController.instance.bullets[3].currentBullets = save.currentBullet_3;
            BulletsController.instance.bullets[4].currentBullets = save.currentBullet_4;*/

            scoreManager.currentScore = save.score;

            Debug.Log("load : " + save.playerPosX + save.playerPosY + save.playerHp + save.currentBullet_0);
        }
    }
}
