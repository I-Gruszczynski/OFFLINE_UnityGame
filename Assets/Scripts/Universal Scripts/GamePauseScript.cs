using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseScript : MonoBehaviour
{
    public GameObject Pause;
    public bool ispaused = false;
    bool cursorstate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ispaused && Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
        else if (ispaused && Input.GetKeyDown(KeyCode.Escape))
        {
            unpause();
        }
    }
    public void unpause()
    {
            ispaused = false;
            Pause.SetActive(false);
            Time.timeScale = 1;
            gameObject.GetComponent<CursorManager>().czyBron=cursorstate;
    }
    
    public void pause()
    {
        Pause.SetActive(true);
        silentpause();
    }
    public void silentpause()
    {
        ispaused = true;
        Time.timeScale = 0;
        cursorstate = gameObject.GetComponent<CursorManager>().czyBron;
        gameObject.GetComponent<CursorManager>().czyBron = false;
    }
    /*
        System podwójnej pauzy , dzia³a ale nie ma potrzeby u¿ywania go


    void Update()
    {
        if (!ispaused && Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
        else if (ispaused && Input.GetKeyDown(KeyCode.Escape))
        {
            unpause();
        }
    }
    public void unpause()
    {
        if (silentpaused && fullpause)
        {
            Pause.SetActive(false);
            fullpause = false;
            return;
        }
            ispaused = false;
            Pause.SetActive(false);
            Time.timeScale = 1;
            gameObject.GetComponent<CursorManager>().czyBron=cursorstate;
    }
    
    public void pause()
    {
        fullpause = true;
        Pause.SetActive(true);
        pausecomponent();
    }
    public void silentpause()
    {
        silentpaused = true;
        pausecomponent();
    }
    public void pausecomponent()
    {
        ispaused = true;
        Time.timeScale = 0;
        cursorstate = gameObject.GetComponent<CursorManager>().czyBron;
        gameObject.GetComponent<CursorManager>().czyBron = false;
    }
    */
}
