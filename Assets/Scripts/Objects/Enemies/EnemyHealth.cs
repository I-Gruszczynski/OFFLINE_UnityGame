using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    public GameObject ammo_prefab;
    Transform BulletsParent;
    public GameObject sword;
    GameObject killcounter;
    //public int grenadeDamage;
    public int swordDamagefromplayer;
    bool colided = false;
    public Animator animator;
    public GameObject pistolHolder;
    public GameObject swordHolder;
    //int hit = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform.Find("Skins"))
        {
            if (child.gameObject.activeSelf)
            {
                animator = child.GetComponent<Animator>();
            }
        }
        killcounter = GameObject.FindGameObjectWithTag("killcounter");
        BulletsParent = GameObject.FindGameObjectWithTag("BNAOS").transform;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Bullet" && other.GetComponent<BulletController>().whocreated == "Player" && !colided)
        {
            colided = true;
            health -= other.GetComponent<BulletController>().DMG;

            if (health <= 0)
            {
                Vector3 position = transform.position;
                GameObject ammo = Instantiate(ammo_prefab, position, Quaternion.identity);
                ammo.transform.SetParent(BulletsParent);
                DIE();
            }
            else
            {
                colided = false;
            }
        }
        /* else if (other.tag == "Grenade")
         {
             health -= grenadeDamage;

             if (health <= 0)
             {
                 killcounter.GetComponent<killcounterscript>().killcount++;
                 Destroy(gameObject);
                 Vector3 position = transform.position;
                 GameObject ammo = Instantiate(ammo_prefab, position, Quaternion.identity);
                 ammo.SetActive(true);

             }
         }*/
        else if (other.tag == "Sword" && !colided && other.GetComponent<Animator>().GetBool("attacking"))
        {
            //sprawdzanie czy uderza podwójnie
            //hit++;
            //Debug.Log(hit);
            colided = true;
            health -= swordDamagefromplayer;
            if (health <= 0)
            {
                DIE();
            }
            else
            {
                colided = false;
            }
        }
    }
    void DIE()
    {
        killcounter.GetComponent<killcounterscript>().killcount++;
        this.gameObject.GetComponent<EnemyMovement>().enabled = false;
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        pistolHolder.SetActive(false);
        swordHolder.SetActive(false);

        animator.SetBool("isDead", true);
        //EnemyCyber.GetComponent<RigBuilder>().enabled = false;
        //EnemySolar.GetComponent<RigBuilder>().enabled = false;
        //EnemySolar2.GetComponent<RigBuilder>().enabled = false;

        Destroy(gameObject, 4);
    }
}
