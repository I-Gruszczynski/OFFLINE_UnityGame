using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTP : MonoBehaviour
{
    [SerializeField]
    public KeyCode modifier;
    public KeyCode key;
    public Vector3 position;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if ((modifier == KeyCode.None && Input.GetKeyDown(key)) || (Input.GetKey(modifier) && Input.GetKeyDown(key)))
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = position;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
