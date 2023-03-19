using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
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
    public GameObject successobj;
    public bool tipfound;
    public bool power;

    // Start is called before the first frame update
    void Start()
    {
        if (tipfound)
        {
            tip.GetComponentInChildren<Text>().text = code;
        }
        if (!tipfound)
            tip.SetActive(false);
        if (power)
        {
            Powerindicator.GetComponent<Image>().color = Color.yellow;
        }
        StartCoroutine(idleanimation());
    }

    // Update is called once per frame
    void Update()
    {
        if (power)
        {
            if (Input.GetKeyDown("1"))
            {
                next=(1);
            }
            if (Input.GetKeyDown("2"))
            {
                next = (2);
            }
            if (Input.GetKeyDown("3"))
            {
                next = (3);

            }
            if (Input.GetKeyDown("4"))
            {
                next = (4);
            }
            if (Input.GetKeyDown("5"))
            {
                next = (5);
            }
            if (Input.GetKeyDown("6"))
            {
                next = (6);
            }
            if (Input.GetKeyDown("7"))
            {
                next = (7);
            }
            if (Input.GetKeyDown("8"))
            {
                next = (8);
            }
            if (Input.GetKeyDown("9"))
            {
                next = (9);
            }
            if (Input.GetKeyDown("0"))
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
                        successobj.SetActive(true);
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
                        successobj.SetActive(true);
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
                    successobj.SetActive(true);
                }
                else
                {
                    badGo.GetComponent<AudioSource>().Play();
                    StartCoroutine(bladanim());
                }
                sprawdz = false;
            }
        }
    }
    IEnumerator idleanimation()
    {
        idle = true;
        while (idle)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            if (!idle) { break; }
            DisplayT.text = "_";
            yield return new WaitForSecondsRealtime(1.5f);
            if (!idle) { break; }
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

    public void usebuttonkeypad(int numer)
    {
        next = numer;
    }

}
