using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F_Dash : MonoBehaviour
{
    public CharacterController controller;
    bool dlewo = false;
    float ltime;
    bool dprawo = false;
    float ptime;
    bool dgora = false;
    float gtime;
    bool ddol = false;
    float dtime;
    public float btwtap = 0.15f;
    float dashed = 0;
    public float cooldown;
    public GameObject cooldown_indicator;
    public Image cooldown_indicator_radial;
    public GamePauseScript GamePauseScript;
    public GameObject dasheffect;
    public float dashSpeed;
    public float dashTime;
    public float immortality_after_dash_time;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //2 buttons instead of double click
        //if (!dlewo&&Input.GetKey("a")&&Input.GetKeyDown("space")){dlewo = true;}

        //lewo
        if (dashed > 0)
        {
            cooldown_indicator.SetActive(true);
            dashed -= Time.deltaTime;
            cooldown_indicator_radial.fillAmount = 1-(dashed / cooldown);
        }
        else
        cooldown_indicator.SetActive(false);

        if(ltime>0)
        {
            ltime -= Time.deltaTime;
            if (Input.GetKey("a")  && dashed <= 0 &&!GamePauseScript.ispaused)
            {
                dlewo = true;
            }
        }
        if ((ltime <= 0) && Input.GetKeyDown("space") &&!GamePauseScript.ispaused)
        {
            ltime = btwtap;
        }

        if (dlewo)
        {
            StartCoroutine(Dash(new Vector3(-1, 0f, 0)));
                dlewo = false;
                dashed = cooldown;
        }
        //prawo
        if (ptime > 0)
        {
            ptime -= Time.deltaTime;
            if (Input.GetKey("d") && dashed <= 0 && !GamePauseScript.ispaused)
            {
                dprawo = true;
            }
        }
        if ((ptime <= 0) && Input.GetKeyDown("space") && !GamePauseScript.ispaused)
        {
            ptime = btwtap;
        }

        if (dprawo)
        {
            StartCoroutine(Dash(new Vector3(1, 0f, 0)));
            dprawo = false;
            dashed = cooldown;
        }


        //gora
        if (gtime > 0)
        {
            gtime -= Time.deltaTime;
            if (Input.GetKey("w") && dashed <= 0 && !GamePauseScript.ispaused)
            {
                dgora = true;
            }
        }
        if ((gtime <= 0) && Input.GetKeyDown("space") && !GamePauseScript.ispaused)
        {
            gtime = btwtap;
        }

        if (dgora)
        {
            StartCoroutine(Dash(new Vector3(0, 0, 1)));
            dgora = false;
            dashed = cooldown;
        }


        //dol
        if (dtime > 0)
        {
            dtime -= Time.deltaTime;
            if (Input.GetKey("s") && dashed <= 0 && !GamePauseScript.ispaused)
            {
                ddol = true;
            }
        }
        if ((dtime <= 0) && Input.GetKeyDown("space") && !GamePauseScript.ispaused)
        {
            dtime = btwtap;
        }

        if (ddol)
        {
            StartCoroutine(Dash(new Vector3(0, 0, -1)));
            ddol = false;
            dashed = cooldown;
        }
    }
    IEnumerator Dash(Vector3 dashdir)
    {
        float startTime = Time.time;
        gameObject.GetComponent<HeartsScript>().immortal = true;
        dasheffect.SetActive(true);
        while(Time.time < startTime + dashTime)
        {
            controller.Move(dashdir * dashSpeed * Time.deltaTime);
            yield return null;
        }
        dasheffect.SetActive(false);
        yield return new WaitForSeconds(immortality_after_dash_time);
        gameObject.GetComponent<HeartsScript>().immortal = false;
    }
}
