using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;
using TMPro;

public class HeartsScript : MonoBehaviour
{
    public GameObject playerModel;
    public int MaxHealthAmmount = 100;
    public float HealthAmmount = 100;
    public float dystans;
    public GameObject UIHearthContainer;
    public GameObject HeartPrefab;
    public Sprite S1img;
    public Sprite S2img;
    public Sprite S3img;
    public Color S1backdot;
    public Color S2backdot;
    public Color S3backdot;
    public int S1sc;
    public int S3sc;
    private int MaxHealthlasttick;
    private float Healthlasttick;
    public GameObject Nadethrower;
    public GameObject WeaponChangerobj;
    public MeshRenderer BackGlow;
    public AudioSource Playeraudiosource;
    public AudioClip hurtsound;
    public float hurtsound_volume;
    public bool immortal = false;
    Animator animator;
    public GameObject holder;
    public GameObject S1indi;
    public GameObject S3indi;

    public Color TipONtext;
    public Color TipOFFtext;
    public Color TipONicon;
    public Color TipOFFicon;

    public bool regen = false;
    //smooth die loading
    private AsyncOperation loadOperation;
    bool dead = false;
    public float regenspeed;
    // Start is called before the first frame update
    void Start()
    {
        UstawSerca();
        fastrefresh();
        MaxHealthlasttick = MaxHealthAmmount;
        Healthlasttick = HealthAmmount;
        animator = playerModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (regen)
        {
            HealthAmmount += regenspeed * Time.deltaTime;
        }
        if (immortal)
        {
            HealthAmmount = Healthlasttick;
        }
        if (Input.GetKey(KeyCode.LeftControl)&&Input.GetKeyDown(KeyCode.Comma))
        {
            HealthAmmount -= 10;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Period))
        {
            HealthAmmount += 10;
        }

        if (HealthAmmount > MaxHealthAmmount)
        {
            HealthAmmount = MaxHealthAmmount;
        }
        else if (HealthAmmount < 0)
        {
            HealthAmmount = 0;
        }
        if (MaxHealthAmmount != MaxHealthlasttick)
        {
            UstawSerca();
        }

        if (HealthAmmount != Healthlasttick)
        {
            fastrefresh();
        }
        MaxHealthlasttick = MaxHealthAmmount;
        Healthlasttick = HealthAmmount;

        if (HealthAmmount <= 0&&!dead)
        {
            StartCoroutine(Die());
        }

        //brak czasu wiêc no xd
        if (Nadethrower.GetComponent<GranadeThrow>().nades_enabled)
        {
            S1indi.GetComponent<TextMeshProUGUI>().color = TipONtext;
            S1indi.transform.GetChild(0).GetComponent<Image>().color = TipONicon;
        }
        else
        {
            S1indi.GetComponent<TextMeshProUGUI>().color = TipOFFtext;
            S1indi.transform.GetChild(0).GetComponent<Image>().color = TipOFFicon;
        }
    }


    void UstawSerca()
    {
        foreach (Transform child in UIHearthContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        float maxha = (MaxHealthAmmount / 10)-1;
        for (int i = 0; i <= maxha; i++)
        {
            GameObject temp = Instantiate(HeartPrefab, UIHearthContainer.transform,false);

            if (i > HealthAmmount / 10 - 1)
            {
                temp.transform.GetChild(0).gameObject.SetActive(false);
            }

            if (i < S3sc)
            {
                temp.transform.GetChild(0).GetComponent<Image>().sprite = S3img;
            }
            else if (i > maxha - S1sc)
            {
                temp.transform.GetChild(0).GetComponent<Image>().sprite = S1img;
            }
            else
            {
                temp.transform.GetChild(0).GetComponent<Image>().sprite = S2img;
            }
            if (i > 0) {
                Vector3 tempvector3 = temp.transform.position;
                tempvector3.x += i * dystans;
                temp.transform.position = tempvector3;
            }
        }
    }
    void fastrefresh()
    {
        for (int i = (MaxHealthAmmount / 10)-1; i > (HealthAmmount / 10) - 1; i--)
        {
            UIHearthContainer.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            if(i > S1sc-1)
            {
                
                Nadethrower.GetComponent<GranadeThrow>().nades_enabled = true;
                BackGlow.material.color = S1backdot;
                BackGlow.material.SetColor("_EmissionColor", S1backdot);
            }
        }

        for (int i = 0; i < HealthAmmount / 10; i++)
        {
            UIHearthContainer.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            UIHearthContainer.transform.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount = 1;
            if (i > S1sc-1)
            {
                Nadethrower.GetComponent<GranadeThrow>().nades_enabled = false;
                BackGlow.material.color = S2backdot;
                BackGlow.material.SetColor("_EmissionColor", S2backdot);
            }
        }
        if (HealthAmmount % 10 != 0)
        {
            UIHearthContainer.transform.GetChild((Mathf.CeilToInt(HealthAmmount / 10)) - 1).GetChild(0).GetComponent<Image>().fillAmount = (HealthAmmount % 10) / 10;
        }

        if (HealthAmmount / 10 <= ((MaxHealthAmmount / 10) - 3))
        {
            WeaponChangerobj.GetComponent<WeaponChanger>().disablesword();
            S3indi.GetComponent<TextMeshProUGUI>().color = TipOFFtext;
            S3indi.transform.GetChild(0).GetComponent<Image>().color = TipOFFicon;
            if (WeaponChangerobj.GetComponent<WeaponChanger>().pistol.activeSelf)
            {
                animator.SetBool("run_pistol", true);
            }
            else
            {
                animator.SetBool("rifle_run", true);
            }
        }
        else
        {
            BackGlow.material.color = S3backdot;
            BackGlow.material.SetColor("_EmissionColor", S3backdot);
            WeaponChangerobj.GetComponent<WeaponChanger>().Sword_enabled = true;
            S3indi.GetComponent<TextMeshProUGUI>().color = TipONtext;
            S3indi.transform.GetChild(0).GetComponent<Image>().color = TipONicon;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!immortal)
        {
            if (other.tag == "Bullet" && other.gameObject.layer == LayerMask.NameToLayer("EnemyProjectiles"))
            {
                Playeraudiosource.PlayOneShot(hurtsound, hurtsound_volume);
                HealthAmmount -= other.GetComponent<BulletController>().DMG;

            }
            else if (other.tag == "Sword" && other.gameObject.layer == LayerMask.NameToLayer("EnemySword"))
            {
                Playeraudiosource.PlayOneShot(hurtsound, hurtsound_volume);
                HealthAmmount -= other.transform.parent.GetComponent<EnemyMovement>().SwordDamage;
            }
        }
    }
    IEnumerator Die()
    {
        dead = true;
        loadOperation = SceneManager.LoadSceneAsync("Main Menu v2");
        loadOperation.allowSceneActivation = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerMousePosition>().enabled = false;
        //playerModel.GetComponent<RigBuilder>().enabled = false;
        holder.SetActive(false);
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(7);
        loadOperation.allowSceneActivation = true;
    }
    
}
