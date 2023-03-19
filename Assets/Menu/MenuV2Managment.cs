using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuV2Managment : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;
    public Animator Camera_animator;
    public Animator Main_character_animator;
    public GameObject PressAnyKey;
    public bool inmenu = false;
    public bool finished = true;
    public GameObject Menubuttonsholder;
    GameObject lastselect;
    public SmoothSceneLoader SmoothSceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        lastselect = new GameObject();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }

        if (Input.anyKeyDown&&!inmenu&&finished)
        {
            SmoothSceneLoader.StartLoadingSceneByName("P1");
            inmenu = true;
            PressAnyKey.SetActive(false);
            Camera_animator.enabled = true;
            Main_character_animator.enabled = true;
            Camera_animator.SetFloat("Direction", 1);
            Main_character_animator.SetFloat("Direction", 1);
            Camera_animator.PlayInFixedTime("MenuCam",-1,0);
            Main_character_animator.PlayInFixedTime("MenuDisableOnCam", -1,0);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inmenu && finished)
        {
            goback();
        }
    }
    public void finishedfalse()
    {
        finished = false;
    }
    
    public void finishedtrue()
    {
        finished = true;
    }

    public void offline()
    {
        foreach(VideoPlayer vp in videoPlayers)
        {
            vp.Pause();
        }
        Menubuttonsholder.SetActive(true);
        Menubuttonsholder.transform.GetChild(0).GetComponent<Button>().Select();
    }
    public void online()
    {
        foreach (VideoPlayer vp in videoPlayers)
        {
            vp.Play();
        }
        Menubuttonsholder.SetActive(false);
    }
    public void goback()
    {
        inmenu = false;
        Camera_animator.SetFloat("Direction", -1);
        Main_character_animator.SetFloat("Direction", -1);
        Camera_animator.PlayInFixedTime("MenuCam", -1, 1);
        Main_character_animator.PlayInFixedTime("MenuDisableOnCam", -1, 1);
        PressAnyKey.SetActive(true);
    }
}
