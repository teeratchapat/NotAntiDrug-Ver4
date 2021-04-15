using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishPoint : MonoBehaviour
{
    public string nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("finish");
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
