using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Image imageDialogueMichal;
    public Image imageDialogueSzymon;
    public Button buttonDialogueNext;
    public Button buttonDialogueYesKill;
    public Button buttonDialogueYesFollow;
    public Button buttonDialogueNo;
    public Button buttonDialogueRewardKill;
    public Button buttonDialogueRewardFollow;

    public Animator animator;
    public DialogueManager dialogueManager;
    public GameObject inRangeNPC;

    public GameObject player;
    public GameObject shotgun;
    public GameObject shotgunModel;
    public GameObject shotgunUI;

    bool isCounting = false;

    public GameObject holder;
    public Texture2D cursorArea;

    public Sprite reward;
    public GameObject exclamationMarkKill;
    public GameObject exclamationMarkFollow;

    public Keypad1 kp;
    public WeaponChanger WeaponChanger;

    public bool completequestTalkKill;
    public bool completequestTalkFollow;

    bool completequestkill;
    bool completequestfollow;

    Animator animatorNPCFollow;
    Animator animatorNPCKill;

    public GameObject NPCfollowModel;
    public GameObject NPCkillModel;

    public bool stopWaving = false;
    bool shotgun_enable;
    public GameObject taskImageSide;

    public Sprite imageDialogue2;
    public Sprite imageDialogue1;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        animatorNPCFollow = NPCfollowModel.GetComponent<Animator>();
        animatorNPCKill = NPCkillModel.GetComponent<Animator>();
    }
    float timer = 0.0f;
    private void Update()
    {
        completequestkill = gameObject.GetComponent<TaskSystem>().completequestkill;
        completequestfollow = gameObject.GetComponent<TaskSystem>().completequestfollow;
        
        shotgun_enable = player.GetComponent<WeaponChanger>().Shotgun_enabled;
        if (isCounting)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
        }
        if (timer >= 3)
        {
            gameObject.GetComponent<TaskSystem>().taskHeaderSide.gameObject.SetActive(false);
            gameObject.GetComponent<TaskSystem>().taskSide.gameObject.SetActive(false);
            gameObject.GetComponent<TaskSystem>().taskImageSide.gameObject.SetActive(false);
            isCounting = false;
            timer = 0;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        inRangeNPC.GetComponent<InteracionNPC>().imageDialogue.sprite = imageDialogue1;
        if (completequestkill && nameText.text == "Szymon" && !completequestTalkKill)
        {
            inRangeNPC.GetComponent<InteracionNPC>().imageDialogue.sprite = imageDialogue2;
            Debug.Log("Jestem tutaj");
            dialogueText.gameObject.SetActive(true);
            animator.SetBool("IsOpen", true);
            dialogueText.text = "Dziêkuje, pokona³eœ tych szalenców. Teraz moge byæ bezpieczny. Proszê, zgodnie z umow¹ strzelba jest twoja.";
            buttonDialogueRewardKill.gameObject.SetActive(true);
            buttonDialogueRewardFollow.gameObject.SetActive(false);
            buttonDialogueNext.gameObject.SetActive(false);
            buttonDialogueNo.gameObject.SetActive(false);
            buttonDialogueYesKill.gameObject.SetActive(false);
            buttonDialogueYesFollow.gameObject.SetActive(false);
            completequestTalkKill = true;
        }
        else if (completequestfollow && nameText.text == "Maja" && !completequestTalkFollow)
        {
            inRangeNPC.GetComponent<InteracionNPC>().imageDialogue.sprite = imageDialogue2;
            dialogueText.gameObject.SetActive(true);
            animator.SetBool("IsOpen", true);
            
            dialogueText.text = "Dziêkuje za odprowadzenie mnie do domu. Kody s¹ twoje, jeszcze raz dziêkujê. Dopilnuj aby ci z Solar City dostali za swoje za to co nam zrobili.";
            buttonDialogueRewardKill.gameObject.SetActive(false);
            buttonDialogueRewardFollow.gameObject.SetActive(true);
            buttonDialogueNext.gameObject.SetActive(false);
            buttonDialogueNo.gameObject.SetActive(false);
            buttonDialogueYesKill.gameObject.SetActive(false);
            buttonDialogueYesFollow.gameObject.SetActive(false);
            completequestTalkFollow = true;
        }
        
        else if(!completequestTalkFollow || !completequestTalkKill)
        {
            if (!completequestTalkKill)
            {
                buttonDialogueRewardKill.gameObject.SetActive(false);
            }
            if (!completequestTalkFollow)
            {
                buttonDialogueRewardFollow.gameObject.SetActive(false);
            }
            Debug.Log("Rozpoczecie questa");
            //dialogueManager.GetComponent<TaskSystem>().task.gameObject.SetActive(false);
            buttonDialogueYesKill.gameObject.SetActive(false);
            buttonDialogueYesFollow.gameObject.SetActive(false);
            buttonDialogueNo.gameObject.SetActive(false);
            animator.SetBool("IsOpen", true);

            Debug.Log(dialogue.name);
            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
                /*
                if(dialogue.name == "Michal")
                {
                    imageDialogueMichal.gameObject.SetActive(true);
                }
                else if(dialogue.name == "Szymon")
                {
                    imageDialogueSzymon.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("Brak NPC");
                }
                */
            }

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 1 && nameText.text == "Szymon")
        {
            buttonDialogueNext.gameObject.SetActive(false);
            buttonDialogueYesKill.gameObject.SetActive(true);
            buttonDialogueYesFollow.gameObject.SetActive(false);
            buttonDialogueNo.gameObject.SetActive(true);
            inRangeNPC.GetComponent<InteracionNPC>().imageDialogue.sprite = imageDialogue2;
        }

        if (sentences.Count == 1 && nameText.text == "Maja")
        {
            buttonDialogueNext.gameObject.SetActive(false);
            buttonDialogueYesKill.gameObject.SetActive(false);
            buttonDialogueYesFollow.gameObject.SetActive(true);
            buttonDialogueNo.gameObject.SetActive(true);
            inRangeNPC.GetComponent<InteracionNPC>().imageDialogue.sprite = imageDialogue2;
        }

        /*
        if ( nameText.text == "EksDi")
        {
            
        }
        */

            if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }



        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentance)
    {
        dialogueText.text = "";
        foreach (char letter in sentance.ToCharArray())
        {
            dialogueText.text = dialogueText.text + letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
        //imageDialogue.gameObject.SetActive(false);
        //nameText.gameObject.SetActive(false);
        //dialogueText.gameObject.SetActive(false);
        //buttonDialogue.gameObject.SetActive(false);
        holder.SetActive(true);
        Vector2 hotspot = new Vector2(cursorArea.width / 2f, cursorArea.height / 2f);
        Cursor.SetCursor(cursorArea, hotspot, CursorMode.Auto);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerMousePosition>().enabled = true;
    }

    public void DialogueTextYesKill()
    {
        exclamationMarkKill.SetActive(false);
        dialogueManager.GetComponent<TaskSystem>().DialogueTextYesKill();
        EndDialogue();
    }

    public void DialogueTextYesFollow()
    {
        exclamationMarkFollow.SetActive(false);
        dialogueManager.GetComponent<TaskSystem>().DialogueTextYesFollow();
        EndDialogue();
    }

    public void DialogueTextNo()
    {
        EndDialogue();
    }

    public void RewardKill()
    {
        player.GetComponent<HeartsScript>().HealthAmmount = 100f;
        shotgun.SetActive(true);
        shotgun_enable = true;
        holder.SetActive(true);
        isCounting = true;
        gameObject.GetComponent<TaskSystem>().taskSide.color = Color.yellow;
        gameObject.GetComponent<TaskSystem>().taskSide.text = "Otrzymano Shotgun";
        gameObject.GetComponent<TaskSystem>().taskImageSide.sprite = reward;
        animator.SetBool("IsOpen", false);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerMousePosition>().enabled = true;
        exclamationMarkKill.SetActive(false);
        buttonDialogueRewardKill.gameObject.SetActive(false);
        taskImageSide.gameObject.GetComponent<Image>().gameObject.SetActive(true);
    }
    public void RewardFollow()
    {
        player.GetComponent<HeartsScript>().HealthAmmount = 100f;
        holder.SetActive(true);
        isCounting = true;
        kp.tipfound = true;
        gameObject.GetComponent<TaskSystem>().taskSide.color = Color.yellow;
        gameObject.GetComponent<TaskSystem>().taskSide.text = "Otrzymano Wskazówkê";
        gameObject.GetComponent<TaskSystem>().taskImageSide.sprite = reward;
        animator.SetBool("IsOpen", false);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerMousePosition>().enabled = true;
        exclamationMarkFollow.SetActive(false);
        buttonDialogueRewardFollow.gameObject.SetActive(false);
        taskImageSide.gameObject.GetComponent<Image>().gameObject.SetActive(true);
    }
}
