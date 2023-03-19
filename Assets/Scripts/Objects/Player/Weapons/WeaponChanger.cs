using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChanger : MonoBehaviour
{
    public GameObject pistol;
    public GameObject rifle;
    public GameObject shotgun;
    public GameObject sword;
    public GameObject UIgo;
    public GameObject Celownik;
    public bool Sword_enabled = false;
    public bool Shotgun_enabled = false;
    public GamePauseScript GamePauseScript;
    public int lastweaponbeforesword=1;

    public GameObject pistolUI;
    public GameObject rifleUI;
    public GameObject shotgunUI;
    public GameObject swordUI;
    public GameObject ammoUI;

    public GameObject dialogueManager;

    int scroll = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            scroll += 1;
            if (scroll > 4)
            {
                scroll = 1;
            }
            if (scroll == 3 && !Shotgun_enabled)
            {
                scroll += 1;
            }
            else if (scroll == 3 && Shotgun_enabled)
            {
                scroll = 3;
            }
            if (scroll == 4 && !Sword_enabled)
            {
                scroll += 1;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            scroll -= 1;
            if (scroll < 1)
            {
                scroll = 4;
            }
            if (scroll == 4 && !Sword_enabled)
            {
                scroll -= 1;
            }
            if (scroll == 3 && !Shotgun_enabled)
            {
                scroll -= 1;
            }
            else if (scroll == 3 && Shotgun_enabled)
            {
                scroll = 3;
            }
        }

        if (!GamePauseScript.ispaused)
        {
                    if ((Input.GetKeyDown(KeyCode.Alpha1)) || scroll == 1 && !pistol.activeSelf)
                    {
                        pistol_change();
                    }

                    if ((Input.GetKeyDown(KeyCode.Alpha2)) || scroll == 2 && !rifle.activeSelf)
                    {
                        rifle_change();
                    }

                    if (Shotgun_enabled && ((Input.GetKeyDown(KeyCode.Alpha3)) || scroll == 3 && !shotgun.activeSelf))
                    {
                        shotgun_change();
                    }

                    if (Sword_enabled && ((Input.GetKeyDown(KeyCode.Alpha4) || scroll == 4 || Input.GetKeyDown(KeyCode.Tilde) || Input.GetKeyDown(KeyCode.BackQuote)) && !sword.activeSelf))
                    {
                        scroll = 4;
                        pistol.SetActive(false);
                        rifle.SetActive(false);
                        shotgun.SetActive(false);
                        sword.SetActive(true);
                        rifle.GetComponent<ShootScript>().waschanged = true;
                        pistol.GetComponent<ShootScript>().waschanged = true;
                        shotgun.GetComponent<ShotgunShootScript>().waschanged = true;
                        Celownik.GetComponent<CursorManager>().czyBron = false;
                        destroyreloadindicators();

                        pistolUI.SetActive(false);
                        rifleUI.SetActive(false);
                        shotgunUI.SetActive(false);
                        swordUI.SetActive(true);
                        ammoUI.SetActive(false);
                    }
            
        }
    }



    void destroyreloadindicators()
    {
        Transform ReloadIndicators = UIgo.transform.Find("Reload_Indicator(Clone)");
        if (ReloadIndicators != null)
        {
            Destroy(ReloadIndicators.gameObject);
        }
    }

    public void disablesword()
    {
        if (Sword_enabled)
        {
            Sword_enabled = false;
            if (lastweaponbeforesword == 1)
            {
                pistol_change();
            }
            else if (lastweaponbeforesword == 2)
            {
                rifle_change();
            }
            else if (lastweaponbeforesword == 3)
            {
                shotgun_change();
            }
        }
    }

    void pistol_change()
    {
        scroll = 1;
        lastweaponbeforesword = 1;
        pistol.SetActive(true);
        rifle.SetActive(false);
        shotgun.SetActive(false);
        sword.SetActive(false);
        rifle.GetComponent<ShootScript>().waschanged = true;
        shotgun.GetComponent<ShotgunShootScript>().waschanged = true;
        Celownik.GetComponent<CursorManager>().czyBron = true;
        destroyreloadindicators();
        
        pistolUI.SetActive(true);
        rifleUI.SetActive(false);
        shotgunUI.SetActive(false);
        swordUI.SetActive(false);
        ammoUI.SetActive(true);
    }
    void rifle_change()
    {
        scroll = 2;
        lastweaponbeforesword = 2;
        pistol.SetActive(false);
        rifle.SetActive(true);
        shotgun.SetActive(false);
        sword.SetActive(false);
        pistol.GetComponent<ShootScript>().waschanged = true;
        shotgun.GetComponent<ShotgunShootScript>().waschanged = true;
        Celownik.GetComponent<CursorManager>().czyBron = true;
        destroyreloadindicators();

        pistolUI.SetActive(false);
        rifleUI.SetActive(true);
        shotgunUI.SetActive(false);
        swordUI.SetActive(false);
        ammoUI.SetActive(true);
    }
    void shotgun_change()
    {
            scroll = 3;
            lastweaponbeforesword = 3;
            pistol.SetActive(false);
            rifle.SetActive(false);
            shotgun.SetActive(true);
            sword.SetActive(false);
            rifle.GetComponent<ShootScript>().waschanged = true;
            pistol.GetComponent<ShootScript>().waschanged = true;
            Celownik.GetComponent<CursorManager>().czyBron = true;
            destroyreloadindicators();

            pistolUI.SetActive(false);
            rifleUI.SetActive(false);
            shotgunUI.SetActive(true);
            swordUI.SetActive(false);
            ammoUI.SetActive(true);
    }
}
