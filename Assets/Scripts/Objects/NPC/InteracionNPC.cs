using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InteracionNPC : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    //public GameObject text3d;

    public Image imageDialogue;
    public Text dialogueText;
    public Text nameText;
    public Button buttonDialogue;

    public GameObject KeyButton;
    public GameObject player;

    public GameObject holder;
    public GameObject exclamationMark;
    public GameObject dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool startquestkill = dialogueManager.GetComponent<TaskSystem>().startquestkill;
        bool startquestfollow = dialogueManager.GetComponent<TaskSystem>().startquestfollow;

        if (isInRange && exclamationMark.activeSelf && !startquestkill && !startquestfollow)
            {
                if (Input.GetKeyDown(interactKey))
                {

                    holder.gameObject.SetActive(false);
                    Cursor.SetCursor(default, Vector2.zero, CursorMode.ForceSoftware);

                    player.GetComponent<PlayerMovement>().enabled = false;
                    player.GetComponent<PlayerMousePosition>().enabled = false;


                    nameText.gameObject.SetActive(true);

                  

                
                    imageDialogue.gameObject.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    buttonDialogue.gameObject.SetActive(true);
                    Debug.Log(imageDialogue.enabled);
                    interactAction.Invoke();
               

            }
            }
            else
            {
            KeyButton.gameObject.SetActive(false);
            }
    }
    //other.gameObject.CompareTag("Player")
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            isInRange = true;
            KeyButton.gameObject.SetActive(true);
            //Debug.Log("InRange");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isInRange = false;
            KeyButton.gameObject.SetActive(false);
            //Debug.Log("OutRange");
        }
    }

}
