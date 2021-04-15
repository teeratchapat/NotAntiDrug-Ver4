using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform grabDetector;
    public Transform grabHoldPoint;
    public float rayDistance = 0.5f;

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetector.position, Vector2.left*transform.localScale.x, rayDistance);
        Debug.DrawRay(grabDetector.position, Vector2.left*transform.localScale.x*rayDistance, Color.red);

        if (grabCheck.collider != null && grabCheck.collider.tag == "Box")
        {
            if (Input.GetKey(KeyCode.F))
            {
                grabCheck.collider.gameObject.transform.parent = grabHoldPoint;
                grabCheck.collider.gameObject.transform.position = grabHoldPoint.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                Physics2D.IgnoreCollision(PlayerMovement.instance.GetComponent<BoxCollider2D>(), grabCheck.collider.gameObject.GetComponent<BoxCollider2D>());
                
            }
            else
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                Physics2D.IgnoreCollision(PlayerMovement.instance.GetComponent<BoxCollider2D>(), grabCheck.collider.gameObject.GetComponent<BoxCollider2D>(),false);
            }
        }
    }
}
