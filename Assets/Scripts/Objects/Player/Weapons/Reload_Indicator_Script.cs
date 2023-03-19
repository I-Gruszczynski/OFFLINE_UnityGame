using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reload_Indicator_Script : MonoBehaviour
{
    public Image Reload_Indicator;
    public float time = 0;
    float currenttime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (time !=0)
        {
            Reload_Indicator.fillAmount = currenttime / time;
            currenttime+=Time.deltaTime;
            if(currenttime > time)
            {
                Destroy(gameObject);
            }
        }
    }
}
