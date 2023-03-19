using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad1 : MonoBehaviour
{
    public AudioSource clicksound;
    public Text DisplayT;
    public int next = -1;
    public char[] displayout = { ' ', ' ', ' ', ' ', ' ', ' ' };
    public string code;
    public bool idle = true;
    bool sprawdz = false;
    public GameObject indicator;
    public GameObject badGo;
    public GameObject tip;
    public GameObject Powerindicator;
    public bool tipfound;
    public bool power;

    public GamePauseScript gamePauseScript;
    public Transform doortransform;
    public GameObject keypadUI;
    public float speed;
    public Vector3 target_position_offset;
    Vector3 targetposition;
    Vector3 startingposition;
    public bool opening = false;
    public bool closing = false;
    bool power_lastframe = false;

    public bool keypadTask = false;

    // Start is called before the first frame update
    void Start()
    {
        startingposition = doortransform.position;
        targetposition = doortransform.position + target_position_offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (tipfound)
        {
            tip.SetActive(true);
            tip.GetComponentInChildren<Text>().text = code;
        }
        else
        {
            tip.SetActive(false);
        }

        if (power)
        {
            if (!power_lastframe)
            {
                Powerindicator.GetComponent<Image>().color = Color.yellow;
                StartCoroutine(idleanimation());
            }
            power_lastframe = true;
            if (keypadUI.activeSelf)
            {
                if (Input.GetKeyDown("1")||Input.GetKeyDown(KeyCode.Keypad1))
                {
                    next = (1);
                }
                if (Input.GetKeyDown("2") || Input.GetKeyDown(KeyCode.Keypad2))
                {
                    next = (2);
                }
                if (Input.GetKeyDown("3") || Input.GetKeyDown(KeyCode.Keypad3))
                {
                    next = (3);

                }
                if (Input.GetKeyDown("4") || Input.GetKeyDown(KeyCode.Keypad4))
                {
                    next = (4);
                }
                if (Input.GetKeyDown("5") || Input.GetKeyDown(KeyCode.Keypad5))
                {
                    next = (5);
                }
                if (Input.GetKeyDown("6") || Input.GetKeyDown(KeyCode.Keypad6))
                {
                    next = (6);
                }
                if (Input.GetKeyDown("7") || Input.GetKeyDown(KeyCode.Keypad7))
                {
                    next = (7);
                }
                if (Input.GetKeyDown("8") || Input.GetKeyDown(KeyCode.Keypad8))
                {
                    next = (8);
                }
                if (Input.GetKeyDown("9") || Input.GetKeyDown(KeyCode.Keypad9))
                {
                    next = (9);
                }
                if (Input.GetKeyDown("0") || Input.GetKeyDown(KeyCode.Keypad0))
                {
                    next = (0);
                }
                if (Input.GetKeyDown("backspace"))
                {
                    next = (-3);
                }
                if (Input.GetKeyDown("return"))
                {
                    next = (-2);
                }
            }

            if (!idle && displayout[0] == ' ' && displayout[1] == ' ' && displayout[2] == ' ' && displayout[3] == ' ' && displayout[4] == ' ' && displayout[5] == ' ')
            {
                StartCoroutine(idleanimation());
            }

            if (next == -3)
            {
                clicksound.Play();
                for (int i = 5; i > 0; i--)
                {
                    displayout[i] = displayout[i - 1];
                }
                displayout[0] = ' ';
                next = -1;
                DisplayT.text = new string(displayout);
            }
            else if (next == -2)
            {
                clicksound.Play();
                sprawdz = true;
                next = -1;
            }
            else if (next != -1)
            {
                idle = false;
                clicksound.Play();
                for (int i = 1; i < 6; i++)
                {
                    displayout[i - 1] = displayout[i];
                }
                displayout[5] = (char)(next + '0');
                next = -1;
                DisplayT.text = new string(displayout);
            }
            if (sprawdz)
            {
                int codetest = 1;
                if (code.Length == 5 && displayout[0] == ' ')
                {
                    //Debug.Log("L5");
                    for (int j = code.Length - 1; j >= 0; j--)
                    {
                        if (displayout[j + 1] != code[j])
                            codetest *= 0;
                    }
                    if (codetest == 1)
                    {
                        indicator.GetComponent<Image>().color = Color.green;
                        indicator.GetComponent<AudioSource>().Play();
                        StartCoroutine(Open());
                    }
                    else
                    {
                        badGo.GetComponent<AudioSource>().Play();
                        StartCoroutine(bladanim());
                    }
                }
                else if (code.Length == 4 && displayout[0] == ' ' && displayout[1] == ' ')
                {
                    //Debug.Log("L4");
                    for (int j = code.Length - 1; j >= 0; j--)
                    {
                        if (displayout[j + 2] != code[j])
                            codetest *= 0;
                    }
                    if (codetest == 1)
                    {
                        indicator.GetComponent<Image>().color = Color.green;
                        indicator.GetComponent<AudioSource>().Play();
                        StartCoroutine(Open());
                    }
                    else
                    {
                        badGo.GetComponent<AudioSource>().Play();
                        StartCoroutine(bladanim());
                    }
                }
                else if (code.Length == 6)
                {
                    for (int j = code.Length - 1; j >= 0; j--)
                    {
                        if (displayout[j] != code[j])
                            codetest *= 0;
                    }
                }
                else
                    codetest = 0;



                if (codetest == 1)
                {
                    indicator.GetComponent<Image>().color = Color.green;
                    indicator.GetComponent<AudioSource>().Play();
                    StartCoroutine(Open());
                }
                else
                {
                    badGo.GetComponent<AudioSource>().Play();
                    StartCoroutine(bladanim());
                }
                sprawdz = false;
            }
        }
        else
        {
            power_lastframe = false;
            Powerindicator.GetComponent<Image>().color = new Color(0.2830189f, 0.249644f, 0.249644f, 1f);
        }
    }
    IEnumerator idleanimation()
    {
        idle = true;
        while (idle)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            if (!idle||!power) { break; }
            DisplayT.text = "_";
            yield return new WaitForSecondsRealtime(1.5f);
            if (!idle||!power) { break; }
            DisplayT.text = " ";
        }
    }
    IEnumerator bladanim()
    {
        indicator.GetComponent<Image>().color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        indicator.GetComponent<Image>().color = new Color(0.2830189f, 0.249644f, 0.249644f, 1f);
        yield return new WaitForSecondsRealtime(0.5f);
        indicator.GetComponent<Image>().color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        indicator.GetComponent<Image>().color = new Color(0.2830189f, 0.249644f, 0.249644f, 1f);
    }

    public IEnumerator Open()
    {
        if (!opening)
        {
            opening = true;
            float step = speed * Time.unscaledDeltaTime;
            while (doortransform.position != targetposition)
            {
                keypadTask = true;
                doortransform.position = Vector3.MoveTowards(doortransform.position, targetposition, step);
                yield return null;
            }
            idle = false;
            opening = false;
            gamePauseScript.unpause();
            keypadUI.SetActive(false);
        }

    }
    public void Openhelper()
    {
        StartCoroutine(Open());
    }
    public void Closehelper()
    {
            StartCoroutine(Close());
    }
    IEnumerator Close()
    {
        if (!closing)
        {
            closing = true;
            float step = speed * Time.deltaTime;
            while (doortransform.position != startingposition)
            {
                doortransform.position = Vector3.MoveTowards(doortransform.position, startingposition, step);
                yield return null;
            }
            closing = false;
        }
    }

    public void usebuttonkeypad(int numer)
    {
        next = numer;
    }

}
