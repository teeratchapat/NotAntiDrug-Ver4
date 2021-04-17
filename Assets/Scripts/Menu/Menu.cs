using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Menu : MonoBehaviour
{
    public string scene_01;
    public string scene_02;
    public string scene_03;

    [SerializeField]
    public List<EnemyDataToSave> enemiesList;

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.level = LevelManager.instance.currentLv;

        //save player
        save.playerPosX = Player.instance.transform.position.x;
        save.playerPosY = Player.instance.transform.position.y;
        save.playerHp = Player.currentHP;

        //save bullets

        save.bulletsLeft = new int[BulletsController.instance.bullets.Count];

        for (int i = 0; i < BulletsController.instance.bullets.Count; i++)
        {
            save.bulletsLeft[i] = BulletsController.instance.bullets[i].currentBullets;
        }

        //save enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("gameObject : " + enemies.Length);
        //Debug.Log("enemies[i] : " + enemies[0].GetComponent<Enemy>().scriptableEnemy.enemyName.GetHashCode());

        for (int i = 0; i < enemies.Length; i++)
        {
            enemiesList.Add(new EnemyDataToSave());
            enemiesList[i].enemyIndex = enemies[i].GetComponent<Enemy>().scriptableEnemy.enemyName.GetHashCode();
            enemiesList[i].enemyPositionX = enemies[i].transform.position.x;
            enemiesList[i].enemyPositionY = enemies[i].transform.position.y;
            enemiesList[i].enemyCurrentHP = enemies[i].GetComponent<Enemy>().enemyHp;
            Debug.Log("loop : 1");
        }

        save.enemiesList = enemiesList;

        Debug.Log("enemy index :" + save.enemiesList.Count);
        save.score = scoreManager.currentScore;

        //Debug.Log("save : " + save.playerPosX + save.playerPosY + save.playerHp + save.currentBullet_0);

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

            LevelManager.isLoadGameSave = true;

            if (save.level == 1)
            {
                SceneManager.LoadScene(scene_01);
            }
            else if (save.level == 2)
            {
                SceneManager.LoadScene(scene_02);
            }
            else if (save.level == 3)
            {
                SceneManager.LoadScene(scene_03);
            }
            Debug.Log("level : " + save.level);

            PlayerMovement.playerStartPosition = new Vector2(save.playerPosX, save.playerPosY);
            Player.currentHP = save.playerHp;

            if (LevelManager.instance.currentLv == 1)
            {
                BulletsController.bullet01_qty = save.bulletsLeft[0];
            }
            else if (LevelManager.instance.currentLv == 2)
            {
                BulletsController.bullet01_qty = save.bulletsLeft[0];
                BulletsController.bullet02_qty = save.bulletsLeft[1];
            }
            else if (LevelManager.instance.currentLv == 3)
            {
                BulletsController.bullet01_qty = save.bulletsLeft[0];
                BulletsController.bullet02_qty = save.bulletsLeft[1];
                BulletsController.bullet03_qty = save.bulletsLeft[2];
                BulletsController.bullet04_qty = save.bulletsLeft[3];
            }

            //Debug.Log("x" + LevelManager.instance.enemyToLoad_Index.Length + "xx" + save.enemiesList.Count);
            
            //Debug.Log("pq = " + EnemyDataToLoad.enemyToLoad_Index.Length);

            scoreManager.currentScore = save.score;

            Debug.Log("load : " + save.playerPosX + save.playerPosY + " " + save.playerHp + save.bulletsLeft[0]);
            Debug.Log("Load+ : " + Player.instance.transform.position.x + Player.instance.transform.position.y);
        }
    }
}