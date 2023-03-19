using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    GameObject go;
    Keypad kp;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown("1"))
        {
            usebuttonkeypad(1);
        }
        if (Input.GetKeyDown("2"))
        {
            usebuttonkeypad(2);
        }
        if (Input.GetKeyDown("3"))
        {
            usebuttonkeypad(3);

        }
        if (Input.GetKeyDown("4"))
        {
            usebuttonkeypad(4);
        }
        if (Input.GetKeyDown("5"))
        {
            usebuttonkeypad(5);
        }
        if (Input.GetKeyDown("6"))
        {
            usebuttonkeypad(6);
        }
        if (Input.GetKeyDown("7"))
        {
            usebuttonkeypad(7);
        }
        if (Input.GetKeyDown("8"))
        {
            usebuttonkeypad(8);
        }
        if (Input.GetKeyDown("9"))
        {
            usebuttonkeypad(9);
        }
        if (Input.GetKeyDown("0"))
        {
            usebuttonkeypad(0);
        }
        if (Input.GetKeyDown("backspace"))
        {
            usebuttonkeypad(-3);
        }
        if (Input.GetKeyDown("return"))
        {
            usebuttonkeypad(-2);
        }*/
    }



    public void usebuttonkeypad(int numer)
    {
        go = GameObject.FindGameObjectWithTag("CodeDisplay");
        kp = go.GetComponent<Keypad>();
        kp.next = numer;
    }

}
