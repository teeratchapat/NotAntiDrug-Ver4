using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    public string mainMenuScene;

    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }
}
