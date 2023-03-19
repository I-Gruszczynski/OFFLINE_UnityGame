using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUIKeypad : MonoBehaviour
{
    public GameObject player;
    public GameObject keypadUI;
    public Keypad1 keypadscript;
    public GamePauseScript GamePauseScript;
    public float show_from_distance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            keypadUI.SetActive(false);
            keypadscript.opening = false;
            keypadscript.closing = false;
            keypadscript.idle = false;
        }
        if (Vector3.Distance(player.transform.position, transform.position) <= show_from_distance)
        {
            transform.Find("KeyButton").gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                keypadUI.SetActive(true);
                GamePauseScript.silentpause();
            }
        }
        else
        {
            transform.Find("KeyButton").gameObject.SetActive(false);
        }
    }
}
