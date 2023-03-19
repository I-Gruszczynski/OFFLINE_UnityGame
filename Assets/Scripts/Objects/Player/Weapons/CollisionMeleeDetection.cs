using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMeleeDetection : MonoBehaviour
{
    Animator anim;
    Animator animEnemy;
    GameObject playerModel;
    GameObject player;
    GameObject enemy;
    GameObject enemyModel;
    public GameObject killcounter;
    public int healthEnemy = 100;
    public int hitHealth = 50;
    public GamePauseScript GamePauseScript;
    int attackcounter=0;
    public int killsword = 0;


    bool hit = false;
    //int wait = 2;
    //int EnemyHealth = 5;
    //public bool isMouse = false;
    private void Start()
    {
        playerModel = GameObject.FindGameObjectWithTag("main_character");
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyModel = GameObject.FindGameObjectWithTag("EnemyModel");
        anim = playerModel.GetComponent<Animator>();
        animEnemy = enemyModel.GetComponent<Animator>();
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !GamePauseScript.ispaused)
        {
            if (attackcounter % 2 == 0)
            {
                anim.SetBool("sword_attack", true);
                anim.SetBool("sword_attack2", false);
                hit = true;
            }
            if (attackcounter % 2 == 1)
            {
                anim.SetBool("sword_attack", false);
                anim.SetBool("sword_attack2", true);
                hit = true;
            }
            attackcounter++;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("sword_attack") || anim.GetCurrentAnimatorStateInfo(0).IsName("sword_attack2") || this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            hit = false;
            anim.SetBool("sword_attack", false);
            anim.SetBool("sword_attack2", false);
        }

    }

    IEnumerator waiter()
    {
        
        yield return new WaitForSeconds(2);

    }

    private void OnTriggerEnter(Collider other)
    {
        Animator Eanimator = other.gameObject.GetComponentInChildren<Animator>();
        EnemyMovement enemyMovement = other.gameObject.GetComponentInChildren<EnemyMovement>();
        if (hit)
        {
            if(other.tag == "Enemy")
            {
                healthEnemy = healthEnemy - hitHealth;
                if (healthEnemy <= 0)
                {
                    Eanimator.SetBool("isDead", true);
                    enemyMovement.enabled = false;
                    Destroy(other.gameObject,4);
                    killsword++;
                    Debug.Log(killsword);
                    healthEnemy = 100;
                }
                else
                {
                    Eanimator.SetBool("isHit", true);
                    StartCoroutine(waiter());
                    Eanimator.SetBool("isHit", false);
                }
            }
        }
    }
}


