using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    float splash_start = 0;
    public GameObject logo;
    public GameObject logoText;
    public GameObject logoGlow;
    private Color logo_col;
    private Color logoText_col;
    private Color logoGlow_col;
    void Start()
    {
        logo_col = logo.GetComponent<SpriteRenderer>().color;
        logoText_col = logoText.GetComponent<SpriteRenderer>().color;
        logoGlow_col = logoGlow.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (splash_start < 6)
        {
            splash_start = splash_start + (Time.deltaTime / 5);
            if (splash_start < 1)
            {
                logo_col.a = splash_start;
                logo.GetComponent<SpriteRenderer>().color = logo_col;
                logoText_col.a = splash_start;
                logoText.GetComponent<SpriteRenderer>().color = logo_col;
                logoGlow_col.a = splash_start;
                logoGlow.GetComponent<SpriteRenderer>().color = logo_col;
            }
            if (splash_start > 1.5f)
            {
                logo_col.a = 1 - (splash_start - 1.5f);
                logo.GetComponent<SpriteRenderer>().color = logo_col;
                logoText_col.a = 1 - (splash_start - 1.5f);
                logoText.GetComponent<SpriteRenderer>().color = logo_col;
                logoGlow_col.a = 1 - (splash_start - 1.5f);
                logoGlow.GetComponent<SpriteRenderer>().color = logo_col;
            }
        }
        if (logo_col.a <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
