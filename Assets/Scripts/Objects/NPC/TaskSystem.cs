using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Text task;
    public Text taskSide;
    public Text taskHeader;
    public Text taskHeaderSide;
    public Image taskImage;
    public Image taskImageSide;

    public Sprite progress;
    public Sprite completed;

    public GameObject killcounter;
    public GameObject NPCfollow;
    public GameObject targetFollow;
    public GameObject player;

    public GameObject lever;
    public GameObject keypad;
    public GameObject boss;

    public float distanceFollow = 6f;
    public float distanceToTarget = 6f;
    public float speed = 3f;
    public bool startquestkill = false;
    public bool startquestfollow = false;
    public bool completequestkill = false;
    public bool completequestfollow = false;

    bool leverTask;
    bool keypadTask;

    public GameObject exclamationMarkKill;
    public GameObject exclamationMarkFollow;
    public GameObject targetFollowObject;

    bool isCounting = false;
    float timer = 0.0f;
    public GameObject NPCfollowModel;

    int killsword;
    public GameObject sword;

    bool alwaysfalse = true;
    bool killboss = false;

    bool zad1Done = false;
    bool zad2Done = false;
    bool zad3Done = false;

    Animator animatorNPC;

    // Start is called before the first frame update
    void Start()
    {
        animatorNPC = NPCfollowModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void DialogueTextYesKill()
    {
        startquestkill = true;
        Debug.Log("Kill");
    }

    public void DialogueTextYesFollow()
    {
        startquestfollow = true;
        Debug.Log("Follow");
    }


    void Update()
    {
        killsword = sword.GetComponent<CollisionMeleeDetection>().killsword;

        leverTask = lever.GetComponent<Keypad1_Lever>().leverTask;
        keypadTask = keypad.GetComponent<Keypad1>().keypadTask;

        //ZAD1
        if (leverTask == false)
        {
            taskHeader.text = "ZADANIA:";
            taskImage.gameObject.SetActive(true);
            taskImage.sprite = progress;
            task.text = "W³¹cz zasilanie u¿ywaj¹c dŸwigni";
            task.gameObject.SetActive(true);
        }
        else if(leverTask == true && zad1Done == false)
        {
            isCounting = true;
            taskHeader.text = "ZADANIA:";
            taskImage.gameObject.SetActive(true);
            taskImage.sprite = completed;
            task.text = "W³¹cz zasilanie u¿ywaj¹c dŸwigni";
            task.gameObject.SetActive(true);
            task.color = new Color(0, 102, 0);

            if (isCounting)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
            }
            if (timer >= 3 && isCounting)
            {
                isCounting = false;
                zad1Done = true;
                taskImage.gameObject.SetActive(false);
                task.gameObject.SetActive(false);
            }
            isCounting = false;
            Debug.Log("Czas to: "+timer);
        }
        //ZAD2
        if (leverTask == true && keypadTask == false && timer >= 3)
        {
            timer = 0;
            Debug.Log("Kolejne zadanie");
            isCounting = false;
            //StartCoroutine(Wait(5));
            taskImage.gameObject.SetActive(true);
            taskImage.sprite = progress;
            task.text = "Znajd¿ kod i otwórz g³ówn¹ bramê";
            task.gameObject.SetActive(true);
            task.color = new Color(255, 255, 255);
        }
        else if(keypadTask == true && zad2Done == false)
        {
            isCounting = true;
            taskImage.gameObject.SetActive(true);
            taskImage.sprite = completed;
            task.text = "Znajd¿ kod i otwórz g³ówn¹ bramê";
            task.gameObject.SetActive(true);
            task.color = new Color(0, 102, 0);

            if (isCounting)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
            }
            if (timer >= 3 && isCounting)
            {
                isCounting = false;
                zad2Done = true;
                taskImage.gameObject.SetActive(false);
                task.gameObject.SetActive(false);
            }

            isCounting = false;

        }
        //ZAD3
        if (keypadTask == true && boss != null && timer >= 3)
        {
            timer = 0;
            Debug.Log("Kolejne zadanie");
            isCounting = false;
            taskImage.gameObject.SetActive(true);
            taskImage.sprite = progress;
            task.text = "Zabij robota";
            task.gameObject.SetActive(true);
            task.color = new Color(255, 255, 255);
        }
        else if(boss == null && zad3Done == false)
        {
            isCounting = true;
            Debug.Log("Jestem tutaj, robot zabity");
            taskImage.gameObject.SetActive(true);
            task.text = "Zabij robota";
            task.gameObject.SetActive(true);
            taskImage.sprite = completed;
            task.color = new Color(0, 102, 0);

            if (isCounting)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
            }
            if (timer >= 3 && isCounting)
            {
                isCounting = false;
                zad3Done = true;
                taskImage.gameObject.SetActive(false);
                task.gameObject.SetActive(false);
            }

            isCounting = false;
            alwaysfalse = false;
        }
        //ZAD4
        if (boss == null && alwaysfalse == false && timer >= 3)
        {
            taskImage.gameObject.SetActive(true);
            taskImage.sprite = progress;
            task.text = "Dostañ siê do serwerowni";
            task.gameObject.SetActive(true);
            task.color = new Color(255, 255, 255);
        }

        if (startquestkill)
        {
            exclamationMarkKill.SetActive(false);
            taskHeaderSide.gameObject.SetActive(true);
            taskHeaderSide.text = "ZADANIA POBOCZNE:";
            taskImageSide.gameObject.SetActive(true);
            taskImageSide.gameObject.GetComponent<Image>().gameObject.SetActive(true);
            taskImageSide.sprite = progress;
            taskSide.color = Color.white;
            animatorNPC.SetBool("isWaving", false);
            taskSide.text = string.Format("Zabij Przeciwników (" + (killcounter.GetComponent<killcounterscript>().killcount + killsword) + "/6)");
            taskSide.gameObject.SetActive(true);

            if ((killcounter.GetComponent<killcounterscript>().killcount + killsword) >= 6)
            {
                taskHeaderSide.gameObject.SetActive(true);
                taskHeaderSide.text = "ZADANIA POBOCZNE:";
                taskImageSide.sprite = completed;
                taskSide.color = new Color(0, 102, 0);
                taskSide.text = string.Format("Wróæ do Szymona aby odebraæ nagrodê");
                startquestkill = false;
                completequestkill = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponChanger>().Shotgun_enabled = true;
                exclamationMarkKill.SetActive(true);
            }
        }
        if (startquestfollow)
        {
            exclamationMarkFollow.SetActive(false);
            targetFollowObject.SetActive(true);       

            taskHeaderSide.gameObject.SetActive(true);
            taskHeaderSide.text = "ZADANIA POBOCZNE:";
            taskImageSide.gameObject.SetActive(true);
            taskImageSide.gameObject.GetComponent<Image>().gameObject.SetActive(true);
            taskImageSide.sprite = progress;
            taskSide.color = Color.white;
            taskSide.text = string.Format("Odeskortuj Maje bezpiecznie do domu");
            taskSide.gameObject.SetActive(true);

            animatorNPC.SetBool("isRunning", true);
            float step = speed * Time.deltaTime;
            float distPlayer = Vector3.Distance(player.transform.position, NPCfollow.transform.position);
            float distTarget = Vector3.Distance(targetFollow.transform.position, NPCfollow.transform.position);


            if (distPlayer <= distanceFollow)
            {
                animatorNPC.SetBool("isRunning", false);
                animatorNPC.SetBool("isWaving", false);

                Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - NPCfollow.transform.position);
                NPCfollow.transform.rotation = Quaternion.Slerp(NPCfollow.transform.rotation, targetRotation, speed * Time.deltaTime);

                NPCfollow.transform.position = NPCfollow.transform.position;
            }
            else if(distTarget <=distanceToTarget)
            {
                animatorNPC.SetBool("isRunning", true);

                Quaternion targetRotation = Quaternion.LookRotation(targetFollow.transform.position - NPCfollow.transform.position);
                NPCfollow.transform.rotation = Quaternion.Slerp(NPCfollow.transform.rotation, targetRotation, speed * Time.deltaTime);

                NPCfollow.transform.position = Vector3.MoveTowards(NPCfollow.transform.position, targetFollow.transform.position, step);

                    if(NPCfollow.transform.position == targetFollow.transform.position)
                    {
                    animatorNPC.SetBool("isRunning", false);
                    animatorNPC.SetBool("isWaving", false);

                    taskHeaderSide.gameObject.SetActive(true);
                    taskHeaderSide.text = "ZADANIA POBOCZNE:";
                    startquestfollow = false;
                    completequestfollow = true;
                    taskImageSide.sprite = completed;
                    taskSide.gameObject.SetActive(false);
                    taskSide.color = new Color(0, 102, 0);
                    taskSide.text = string.Format("Odeskortowa³eœ Micha³a, mo¿esz odebraæ nagrodê");
                    taskSide.gameObject.SetActive(true);
                    exclamationMarkFollow.SetActive(true);
                    targetFollowObject.SetActive(false);
                }
            }
            else
            {
                animatorNPC.SetBool("isRunning", true);

                Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - NPCfollow.transform.position);
                NPCfollow.transform.rotation = Quaternion.Slerp(NPCfollow.transform.rotation, targetRotation, speed * Time.deltaTime);

                NPCfollow.transform.position = Vector3.MoveTowards(NPCfollow.transform.position, player.transform.position, step);
            }
            
        }

        
    }

    IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}

