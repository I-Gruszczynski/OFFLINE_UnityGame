using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class WajchaLauncher : MonoBehaviour
{
    public VideoPlayer CRTPLAYER;
    public Animator DialogAniamtor;
    public Text Dialogue;
    public Button ButtonNext;
    public Button ButtonYes;
    public Button ButtonNo;

    public Sprite imageDialogue;
    public GameObject Dialogbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CRTPLAYER.loopPointReached += EndReached;
    }
    public void DialogNoCRT()
    {

        CRTPLAYER.enabled = true;
    }
    void EndReached(VideoPlayer vp)
    {
        Application.Quit();
    }
    void YesDialogAfterswitch()
    {
        Dialogbox.GetComponent<Image>().sprite = imageDialogue;
        DialogAniamtor.SetBool("IsOpen", true);
        Dialogue.text = "Internet przywrócony. Uda³o mi siê wszystkich uratowaæ. A ja wreszcie mogê zagraæ w moj¹ ulubion¹ grê.";
        ButtonNext.gameObject.SetActive(true);
        ButtonNo.gameObject.SetActive(false);
        ButtonYes.gameObject.SetActive(false);
    }
}
