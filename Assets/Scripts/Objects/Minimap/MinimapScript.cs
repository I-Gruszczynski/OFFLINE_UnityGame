using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public GameObject player;
    public int step;
    public int minzoom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus) && gameObject.transform.position.y < minzoom)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y + step, player.transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.Equals)&&gameObject.transform.position.y>5)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y - step, player.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
        }
    }
}
