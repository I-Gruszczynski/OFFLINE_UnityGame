using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Video;
public class EndingLever : MonoBehaviour
{
    public Animator wajchaanimator;
    public Dialogue dialogue;
    public GameObject DialogueManager;
    public Animator DialogAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TEST ZONE
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad1))
        {
            DialogYes();
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad2))
        {
            DialogNo();
        }
    }
    public void AnimationEnded()
    {
        DialogueManager.GetComponent<Poziom2DialogueManager>().StartDialogue(dialogue);
    }


    //Wybieramy by w³¹czyæ internet
    public void DialogYes()
    {
        wajchaanimator.enabled = true;
        wajchaanimator.Play("EndingLeverAnimation");
        DialogAnimator.SetBool("IsOpen", false);
        
    }


    //Wybieramy by zniszczyæ internet
    public void DialogNo()
    {
        wajchaanimator.enabled = true;
        wajchaanimator.Play("EndingLeverAnimationNegative");
        DialogAnimator.SetBool("IsOpen", false);
    }
}
