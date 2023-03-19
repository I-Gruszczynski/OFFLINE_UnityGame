using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorCycle : MonoBehaviour
{
    [SerializeField] Light xlight;
    [SerializeField] float seconds;
    float timer = 0.0f;
    bool blueToGreen = true;
    bool greenToRed = false;
    bool redToBlue = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime / seconds;

        if (blueToGreen == true && greenToRed == false && redToBlue == false)
        {
            xlight.color = Color.Lerp(Color.blue, Color.green, timer);
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                blueToGreen = false;
                greenToRed = true;
            }
        }

        if (greenToRed == true && blueToGreen == false && redToBlue == false)
        {
            xlight.color = Color.Lerp(Color.green, Color.red, timer);
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                greenToRed = false;
                redToBlue = true;
            }
        }

        if (redToBlue == true && greenToRed == false && blueToGreen == false)
        {
            xlight.color = Color.Lerp(Color.red, Color.blue, timer);
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                redToBlue = false;
                blueToGreen = true;
            }
        }
    }
}
