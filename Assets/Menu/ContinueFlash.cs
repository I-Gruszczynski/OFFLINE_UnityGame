using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContinueFlash : MonoBehaviour
{
    public float seconds;
    float timer = 0.0f;
    bool BTW=true;
    bool WTB=false;
    public TextMeshProUGUI TMP;
    // Start is called before the first frame update
    void Start()
    {
        TMP.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime / seconds;

        if (BTW && !WTB)
        {
            TMP.color = Color.Lerp(Color.black, Color.white, timer);
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                BTW = false;
                WTB = true;
            }
        }

        if (!BTW && WTB)
        {
            TMP.color = Color.Lerp(Color.white, Color.black, timer);
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                WTB = false;
                BTW = true;
            }
        }
    }
}
