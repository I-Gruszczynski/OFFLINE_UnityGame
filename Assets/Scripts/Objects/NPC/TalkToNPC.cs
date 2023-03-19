using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkToNPC : MonoBehaviour
{
    public GameObject Text3d;
    public Dialogue dialogue;
    public GameObject DialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>().StartDialogue(dialogue);

    }
}

