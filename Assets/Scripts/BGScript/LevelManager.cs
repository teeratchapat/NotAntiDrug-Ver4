using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private Checkpoint[] checkpoints;

    public static bool isLoadGameSave;

    public Vector3 spawnPoint;
    private float waitToRespawn = 2;

    public int[] enemyToLoad_Index;
    public float[] enemyToLoad_HP;
    public Vector2[] enemyToLoad_Position;
    
    public int currentLv;
    public Transform[] enemySpawnPoint;

    public GameObject Alcohol;
    public GameObject Glue;
    public GameObject Opium;
    public GameObject Amphetamine;
    public GameObject Cocaine;
    public GameObject Ecstasy;
    public GameObject Mitragynine;
    public GameObject MagicMushroom;

    [System.Serializable]
    public class EnemyToSpawn
    {
        public ScriptableEnemy.EnemyName enemyName;
        public float spawnRate;

        public EnemyToSpawn(ScriptableEnemy.EnemyName enemyName, float spawnRate)
        {
            this.enemyName = enemyName;
            this.spawnRate = spawnRate;
        }
    }

    public EnemyToSpawn[] enemySpawnRate = {
        new EnemyToSpawn(ScriptableEnemy.EnemyName.Alcohol,0),
        new EnemyToSpawn(ScriptableEnemy.EnemyName.Glue,0),
        new EnemyToSpawn(ScriptableEnemy.EnemyName.Opium,0),
        new EnemyToSpawn(ScriptableEnemy.EnemyName.Amphetamine,0),
        new EnemyToSpawn(ScriptableEnemy.EnemyName.Cocaine,0),
        new EnemyToSpawn(ScriptableEnemy.EnemyName.Ecstasy,0),
        new EnemyToSpawn(ScriptableEnemy.EnemyName.Mitragynine,0),
        new EnemyToSpawn(ScriptableEnemy.EnemyName.MagicMushroom,0),
    };
    public float sumOfEnemySpawnRate;
    public int numOfEnemyToSpawn;
    public ScriptableEnemy.EnemyName[] enemiesToSpawn;

    /*public ScriptableEnemy.EnemyName[] enemiesToSpawnLvl_01 = {
        ScriptableEnemy.EnemyName.Alcohol,    ScriptableEnemy.EnemyName.Alcohol,    ScriptableEnemy.EnemyName.Alcohol,    ScriptableEnemy.EnemyName.Alcohol,    ScriptableEnemy.EnemyName.Alcohol,    ScriptableEnemy.EnemyName.Alcohol,    ScriptableEnemy.EnemyName.Alcohol,  ScriptableEnemy.EnemyName.Alcohol,  ScriptableEnemy.EnemyName.Alcohol,  ScriptableEnemy.EnemyName.Alcohol,
        ScriptableEnemy.EnemyName.Glue,       ScriptableEnemy.EnemyName.Glue,       ScriptableEnemy.EnemyName.Glue,       ScriptableEnemy.EnemyName.Glue,       ScriptableEnemy.EnemyName.Glue,       ScriptableEnemy.EnemyName.Glue,       ScriptableEnemy.EnemyName.Glue,     ScriptableEnemy.EnemyName.Glue,     ScriptableEnemy.EnemyName.Glue,     ScriptableEnemy.EnemyName.Glue,
        ScriptableEnemy.EnemyName.Opium,      ScriptableEnemy.EnemyName.Opium,      ScriptableEnemy.EnemyName.Opium,      ScriptableEnemy.EnemyName.Opium,      ScriptableEnemy.EnemyName.Opium,      ScriptableEnemy.EnemyName.Opium,      ScriptableEnemy.EnemyName.Opium,    ScriptableEnemy.EnemyName.Opium,    ScriptableEnemy.EnemyName.Opium,
        };*/

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();

        spawnPoint = Player.instance.transform.position;

        sumOfEnemySpawnRate = 0;

        if (isLoadGameSave)
        {
            LoadSavedEnemy();
            //Debug.Log("q = " + EnemyDataToLoad.enemyToLoadQueue.Count);
        }
        else
        {
            RandomSpawnEnemy();
        }
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        Player.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);

        Player.instance.gameObject.SetActive(true);
        Player.currentHP = Player.instance.maxHP;
        Player.instance.transform.position = spawnPoint;
        AudioManager.instance.PlaySfx(12);
        FindObjectOfType<HPbarUI>().updateHpUI();
        FindObjectOfType<BulletsUI>().updateBulletUI();
    }




    public void DeactiveCheckPoint()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckPoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    #region random spawn enemy
    //random enemy and spawn it to each position
    public void RandomSpawnEnemy()
    {

        /*if (currentLv == 1)
        {
            /*Debug.Log("sss " + enemiesToSpawnLvl_01.Length + " " + enemySpawnPoint.Length);
            //Reorder enemyList
            for (int i = 0; i < enemiesToSpawnLvl_01.Length - 1; i++)
            {
                int rnd = Random.Range(i, enemiesToSpawnLvl_01.Length - 1);
                ScriptableEnemy.EnemyName tempEnemyIdToSpawn = enemiesToSpawnLvl_01[rnd];
                enemiesToSpawnLvl_01[rnd] = enemiesToSpawnLvl_01[i];
                enemiesToSpawnLvl_01[i] = tempEnemyIdToSpawn;
            }

            //replace last 3 id with 0
            enemiesToSpawnLvl_01[enemySpawnPoint.Length - 3] = 0;
            enemiesToSpawnLvl_01[enemySpawnPoint.Length - 2] = 0;
            enemiesToSpawnLvl_01[enemySpawnPoint.Length - 1] = 0;

            /*for (int i = 0; i < enemiesToSpawnLvl_01.Length; i++)
            {
                Debug.Log("enemiesToSpawnLvl_01 " + i + " : " + enemiesToSpawnLvl_01[i]);
            }*/

        //reorder enemylist again
        /*for (int i = 0; i < enemySpawnPoint.Length - 1; i++)
        {
            int rnd = Random.Range(i, enemiesToSpawnLvl_01.Length);
            ScriptableEnemy.EnemyName tempEnemyIdToSpawn = enemiesToSpawnLvl_01[rnd];
            enemiesToSpawnLvl_01[rnd] = enemiesToSpawnLvl_01[i];
            enemiesToSpawnLvl_01[i] = tempEnemyIdToSpawn;

            //Debug.Log("enemiesToSpawnLvl_01 " + i + " : " + enemiesToSpawnLvl_01[i]);
            switch (enemiesToSpawnLvl_01[i])
            {
                case 0:
                    break;
                case ScriptableEnemy.EnemyName.Alcohol:
                    Instantiate(Alcohol, enemySpawnPoint[i].position, Quaternion.identity);
                    Debug.Log("spawn : Alcohol ");
                    break;
                case ScriptableEnemy.EnemyName.Glue:
                    Instantiate(Glue, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.Opium:
                    Instantiate(Opium, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.Amphetamine:
                    Instantiate(Amphetamine, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.Cocaine:
                    Instantiate(Cocaine, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.Ecstasy:
                    Instantiate(Ecstasy, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.Mitragynine:
                    Instantiate(Mitragynine, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.MagicMushroom:
                    Instantiate(MagicMushroom, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
    }*/
        //add enemies&space to array
        for (int i = 0; i < enemySpawnRate.Length; i++)
        {
            sumOfEnemySpawnRate += enemySpawnRate[i].spawnRate;
        }

        enemiesToSpawn = new ScriptableEnemy.EnemyName[enemySpawnPoint.Length];

        for (int i = 0; i < enemySpawnPoint.Length; i++)
        {
            if (i < numOfEnemyToSpawn)
            {
                float rnd = Random.Range(0, sumOfEnemySpawnRate);
                //Debug.Log("Spawn(rnd) : " + rnd);

                //add enemy
                int enemyIndex = 0;
                float counter = 0;

                while (rnd > counter)
                {
                    counter += enemySpawnRate[enemyIndex].spawnRate;
                    enemyIndex += 1;
                }

                switch (enemyIndex)
                {
                    case 1:
                        enemiesToSpawn[i] = ScriptableEnemy.EnemyName.Alcohol;
                        break;
                    case 2:
                        enemiesToSpawn[i] = ScriptableEnemy.EnemyName.Glue;
                        break;
                    case 3:
                        enemiesToSpawn[i] = ScriptableEnemy.EnemyName.Opium;
                        break;
                    case 4:
                        enemiesToSpawn[i] = ScriptableEnemy.EnemyName.Amphetamine;
                        break;
                    case 5:
                        enemiesToSpawn[i] = ScriptableEnemy.EnemyName.Cocaine;
                        break;
                    case 6:
                        enemiesToSpawn[i] = ScriptableEnemy.EnemyName.Ecstasy;
                        break;
                    case 7:
                        enemiesToSpawn[i] = ScriptableEnemy.EnemyName.Mitragynine;
                        break;
                    case 8:
                        enemiesToSpawn[i] = ScriptableEnemy.EnemyName.MagicMushroom;
                        break;
                    default:
                        break;
                }
                Debug.Log("rnd : " + rnd + "/" + counter + " spawn : " + enemyIndex);
            }
            else
            {
                enemiesToSpawn[i] = ScriptableEnemy.EnemyName.none;
            }
        }

        //suffle
        for (int i = 0; i < enemySpawnPoint.Length - 1; i++)
        {
            int rnd = Random.Range(i, enemiesToSpawn.Length);
            ScriptableEnemy.EnemyName tempEnemyIdToSpawn = enemiesToSpawn[rnd];
            enemiesToSpawn[rnd] = enemiesToSpawn[i];
            enemiesToSpawn[i] = tempEnemyIdToSpawn;

            //Debug.Log("enemiesToSpawnLvl_01 " + i + " : " + enemiesToSpawnLvl_01[i]);
            switch (enemiesToSpawn[i])
            {
                case 0:
                    break;
                case ScriptableEnemy.EnemyName.Alcohol:
                    Instantiate(Alcohol, enemySpawnPoint[i].position, Quaternion.identity);
                    Debug.Log("spawn : Alcohol ");
                    break;
                case ScriptableEnemy.EnemyName.Glue:
                    Instantiate(Glue, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.Opium:
                    Instantiate(Opium, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.Amphetamine:
                    Instantiate(Amphetamine, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.Cocaine:
                    Instantiate(Cocaine, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.Ecstasy:
                    Instantiate(Ecstasy, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.Mitragynine:
                    Instantiate(Mitragynine, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                case ScriptableEnemy.EnemyName.MagicMushroom:
                    Instantiate(MagicMushroom, enemySpawnPoint[i].position, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }

    }
    #endregion

    public void LoadSavedEnemy()
    {
        if (File.Exists(Application.dataPath + "/data.text"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            //FileStream fileStream = File.Open(Application.persistentDataPath + "/data.text", FileMode.Open);
            FileStream fileStream = File.Open(Application.dataPath + "/data.text", FileMode.Open);

            Save save = binaryFormatter.Deserialize(fileStream) as Save;

            fileStream.Close();

            for (int i = 0; i < save.enemiesList.Count; i++)
            {
                GameObject clone;
                switch (save.enemiesList[i].enemyIndex)
                {
                    case 0:
                        break;
                    case (int)ScriptableEnemy.EnemyName.Alcohol:
                        clone = (GameObject)Instantiate(Alcohol,new Vector2( save.enemiesList[i].enemyPositionX,save.enemiesList[i].enemyPositionY), Quaternion.identity);
                        clone.GetComponent<Enemy>().enemyHp = save.enemiesList[i].enemyCurrentHP;
                        Debug.Log("spawn : Alcohol ");
                        break;
                    case (int)ScriptableEnemy.EnemyName.Glue:
                        clone = (GameObject)Instantiate(Glue, new Vector2( save.enemiesList[i].enemyPositionX,save.enemiesList[i].enemyPositionY), Quaternion.identity);
                        clone.GetComponent<Enemy>().enemyHp =  save.enemiesList[i].enemyCurrentHP;
                        break;
                    case (int)ScriptableEnemy.EnemyName.Opium:
                        clone = (GameObject)Instantiate(Opium, new Vector2( save.enemiesList[i].enemyPositionX,save.enemiesList[i].enemyPositionY), Quaternion.identity);
                        clone.GetComponent<Enemy>().enemyHp =  save.enemiesList[i].enemyCurrentHP;
                        break;
                    case (int)ScriptableEnemy.EnemyName.Amphetamine:
                        clone = (GameObject)Instantiate(Amphetamine, new Vector2( save.enemiesList[i].enemyPositionX,save.enemiesList[i].enemyPositionY), Quaternion.identity);
                        clone.GetComponent<Enemy>().enemyHp =  save.enemiesList[i].enemyCurrentHP;
                        break;
                    case (int)ScriptableEnemy.EnemyName.Cocaine:
                        clone = (GameObject)Instantiate(Cocaine, new Vector2( save.enemiesList[i].enemyPositionX,save.enemiesList[i].enemyPositionY), Quaternion.identity);
                        clone.GetComponent<Enemy>().enemyHp =  save.enemiesList[i].enemyCurrentHP;
                        break;
                    case (int)ScriptableEnemy.EnemyName.Ecstasy:
                        clone = (GameObject)Instantiate(Ecstasy, new Vector2( save.enemiesList[i].enemyPositionX,save.enemiesList[i].enemyPositionY), Quaternion.identity);
                        clone.GetComponent<Enemy>().enemyHp =  save.enemiesList[i].enemyCurrentHP;
                        break;
                    case (int)ScriptableEnemy.EnemyName.Mitragynine:
                        clone = (GameObject)Instantiate(Mitragynine, new Vector2( save.enemiesList[i].enemyPositionX,save.enemiesList[i].enemyPositionY), Quaternion.identity);
                        clone.GetComponent<Enemy>().enemyHp =  save.enemiesList[i].enemyCurrentHP;
                        break;
                    case (int)ScriptableEnemy.EnemyName.MagicMushroom:
                        clone = (GameObject)Instantiate(MagicMushroom, new Vector2( save.enemiesList[i].enemyPositionX,save.enemiesList[i].enemyPositionY), Quaternion.identity);
                        clone.GetComponent<Enemy>().enemyHp =  save.enemiesList[i].enemyCurrentHP;
                        break;
                    default:
                        break;
                }
            }
        }
            //Debug.Log("q = " + EnemyDataToLoad.enemyToLoadQueue.Count);
            /*
            for (int i = 0; i < EnemyDataToLoad.instance.enemyToLoadQueue.Count; i++)
            {
                EnemyDataToLoad.EnemyToLoad enemyToLoad = EnemyDataToLoad.instance.enemyToLoadQueue.Dequeue();
                
            }
            */
        }
}
