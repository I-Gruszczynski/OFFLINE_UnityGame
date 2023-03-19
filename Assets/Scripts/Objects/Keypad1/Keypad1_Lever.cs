using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad1_Lever : MonoBehaviour
{
    public GameObject player;
    public float show_from_distance;
    public Keypad1 keypad1;
    public float speed;
    public float degrees;
    Transform leverRod;
    bool goup = false;
    bool godown = false;

    public bool leverTask = false;
    Quaternion temp;
    // Start is called before the first frame update
    void Start()
    {
        leverRod = transform.Find("LeverRod").transform;
        temp = leverRod.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //JEŒLI ¯YCIE CI MI£E TO NIE DOTYKAJ
        if (goup)
        {
            if (leverRod.rotation.x < 0.9535)
            {
                temp.x += speed * Time.deltaTime;
                leverRod.transform.rotation = temp;
            }
            else
            {
                keypad1.power = true;
                goup = false;
            }
        }
        else if (godown)
        {
            if (leverRod.rotation.x > 0.3007058)
            {
                temp.x -= speed * Time.deltaTime;
                leverRod.transform.rotation = temp;
            }
            else
            {
                keypad1.power = false;
                godown = false;
            }
        }

        //up 0,953717
        //down : rotationx:0,3007058
        //Debug.Log("eulerandgles: " +leverRod.eulerAngles.x + " rotation x " + leverRod.rotation.x);
        if (Vector3.Distance(player.transform.position, transform.position) <= show_from_distance)
        {
            transform.Find("KeyButton").gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                leverTask = true;
                if (!godown && !goup)
                {
                    if(leverRod.rotation.x >= 0.9535)
                    {
                        godown = true;
                    }
                    else
                    {
                        goup = true;
                    }
                }
            }
        }
        else
        {
            transform.Find("KeyButton").gameObject.SetActive(false);
        }
    }

    IEnumerator switchleverup()
    {
        Debug.Log("Coroutine");

        while (leverRod.rotation.x < 0.953717)
        {
            Debug.Log("tempx: " + temp.x + "speed * Time.deltaTime: " + speed * Time.deltaTime + "razem: " + temp.x + speed * Time.deltaTime);
            temp.x += speed * Time.deltaTime;
            leverRod.transform.rotation = temp;
        }
        yield return null;
    }
}
