using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkToPlayer : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject DialogueManager;
    public GameObject holder;
    float x;
    float z;
    bool dialogueDone;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        dialogueDone = true;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        x = gameObject.GetComponent<PlayerMovement>().x;
        z = gameObject.GetComponent<PlayerMovement>().z;

            if (dialogueDone)
            {
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                gameObject.GetComponent<PlayerMousePosition>().enabled = false;
                Cursor.SetCursor(default, Vector2.zero, CursorMode.ForceSoftware);
                holder.gameObject.SetActive(false);
                animator.SetBool("run_pistol", false);
                TriggerDialogue();
                dialogueDone = false;
            }

    }

    public void TriggerDialogue()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "P2")
        {
            DialogueManager.GetComponent<Poziom2DialogueManager>().StartDialogue(dialogue);
        }
        if (scene.name == "P1")
        {
            DialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogue);
        }
    }
}
