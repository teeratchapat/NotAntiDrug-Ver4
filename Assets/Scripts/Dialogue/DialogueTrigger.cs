using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject talkIcon;

    public List<Dialogue> dialogues;
    public bool isPlayerInRange;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)&&isPlayerInRange)
        {
            if (DialogueManager.instance.isTalking == false)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogues);
            }

            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player in range");
            isPlayerInRange = true;
            talkIcon.gameObject.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player left range");
            isPlayerInRange = false;
            talkIcon.gameObject.SetActive(false);
        }
    }
}