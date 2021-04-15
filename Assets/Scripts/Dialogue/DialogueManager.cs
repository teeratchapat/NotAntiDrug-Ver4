using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    
    public Queue<Dialogue> dialogues;

    public Dialogue dialogue;

    public bool isTalking = false;

    [Header("UI Component")]
    public GameObject dialogBox;
    public Text NameText;
    public Text dialogText;
    public Image speakerImage;
    public GameObject insertImageBox;
    public Image insertImage;

    [Header("Sprite")]
    public Sprite PlayerSprite, DoctorSprite, Boss1Sprite, Boss2Sprite, Boss3Sprite;

    void Start()
    {
        instance = this;

        dialogues = new Queue<Dialogue>();
    }

    public void StartDialogue(List<Dialogue> dialogues)
    {
        isTalking = true;
        //Debug.Log("Starting conversation with "+dialogue.speaker.ToString());
        dialogBox.SetActive(true);
        //Player.instance.gameObject.SetActive(false);
        Time.timeScale = 0;

        //NameText.text = dialogue.speaker.ToString();

        foreach(Dialogue sentance in dialogues)
        {
            this.dialogues.Enqueue(sentance);
        }
    }

    public void DisplayNextSentence()
    {
        Debug.Log("DisplayNextSentence : " + dialogues.Count + "," + dialogues.Count);
        if (dialogues.Count == 0)
        {
            EndDialogue();
        }
        
        //
        dialogue = dialogues.Dequeue();
        
        //set name on UI
        NameText.text = Dialogue.inGameNameSpeaker[dialogue.speaker.GetHashCode()];
        
        //set speakerImage on UI
        switch (dialogue.speaker)
        {
            case Dialogue.Speaker.player:
                speakerImage.sprite = PlayerSprite;
                break;
            case Dialogue.Speaker.doctor:
                speakerImage.sprite = DoctorSprite;
                break;
            case Dialogue.Speaker.Boss1:
                speakerImage.sprite = Boss1Sprite;
                break;
            case Dialogue.Speaker.Boss2:
                speakerImage.sprite = Boss2Sprite;
                break;
            case Dialogue.Speaker.Boss3:
                speakerImage.sprite = Boss3Sprite;
                break;
        }

        //set dialogText on UI
        dialogText.text = dialogue.quote;
        Debug.Log(dialogue.quote);

        //set insertImage on UI
        if(dialogue.insertImage == null)
        {
            insertImageBox.gameObject.SetActive(false);
        }
        else
        {
            insertImageBox.gameObject.SetActive(true);
            insertImage.sprite = dialogue.insertImage;
        }
    }

    public void EndDialogue()
    {
        isTalking = false;
        Debug.Log("End of conversation");
        dialogBox.SetActive(false);
        Player.instance.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}