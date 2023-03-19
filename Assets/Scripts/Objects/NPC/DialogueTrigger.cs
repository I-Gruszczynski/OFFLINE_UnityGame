using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject NPC;
    
    public void TriggerDialogue()
    {
        NPC.GetComponent<DialogueManager>().StartDialogue(dialogue);
    }
}
