using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingDoorScript : MonoBehaviour
{
    public float show_from_distance;
    GameObject player;
    GameObject keybutton;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        keybutton = transform.Find("KeyButton").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= show_from_distance)
        {
            Debug.Log("test");
            keybutton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Ending", LoadSceneMode.Single);
            }
        }
        else
        {
            keybutton.SetActive(false);
        }
    }
}
