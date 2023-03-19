using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevLVLENTER : MonoBehaviour
{
    int ctrlcounter = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ctrlcounter);
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                ctrlcounter++;
                if(ctrlcounter >= 5)
                {
                    SceneManager.LoadScene("Dev");
                }
            }
            else
            {
                ctrlcounter = 0;
            }
        }
    }
}
