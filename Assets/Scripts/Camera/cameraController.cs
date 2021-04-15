using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform target;
    public Transform BG;

    public float cameraXmin = 0f;
    public float cameraXmax = 1000f;

    public float cameraYmin = 0f;
    public float cameraYmax = 1000f;


    public float cameraX;
    public float cameraY;

    public float cameraOffset = 5f;

    void Update()
    {

        if (target.position.x < transform.position.x - cameraOffset)
        {
            cameraX = target.position.x + cameraOffset;
        }
        else if (target.position.x > transform.position.x + cameraOffset)
        {
            cameraX = target.position.x - cameraOffset;
        }

        cameraY = target.position.y+2;

        Mathf.Clamp(cameraX, cameraXmin, cameraXmax);
        Mathf.Clamp(cameraY, cameraYmin, cameraYmax);

        transform.position = new Vector3(cameraX, cameraY, transform.position.z);
        BG.position = new Vector3(cameraX, cameraY, BG.position.z);
    }
}
