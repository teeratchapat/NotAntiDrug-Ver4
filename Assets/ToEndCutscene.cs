using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToEndCutscene : MonoBehaviour
{
    public GameObject endCutscene;
    public void toEndCutscene()
    {
        endCutscene.gameObject.SetActive(true);
    }
}
