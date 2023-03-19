using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad1CodeTip : MonoBehaviour
{
    public GameObject player;
    public float show_from_distance;
    public Keypad1 keypad1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!keypad1.tipfound)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= show_from_distance)
            {
                transform.Find("KeyButton").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(pickedup());
                 keypad1.tipfound = true;
                }
            }
            else
            {
                transform.Find("KeyButton").gameObject.SetActive(false);
            }
        }
    }
    IEnumerator pickedup()
    {
        transform.Find("KeyButton").gameObject.GetComponent<TextMesh>().text = "Poniesiono wskazówkê";
        yield return new WaitForSeconds(2);
        transform.Find("KeyButton").gameObject.GetComponent<TextMesh>().text = "[E]";
        transform.Find("KeyButton").gameObject.SetActive(false);

    }



}
