using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    NavMeshAgent agent;
    Transform player;
    Animator sword_animator;
    Transform BulletsParent;
    bool hasshot = false;
    bool hasmeleed = false;
    public int rangefromplayerstop;
    public int rangefromplayerstart;
    public float melee_stop_range;
    public float betweenshotsmin;
    public float betweenshotsmax;
    public float betweenmeleesmin;
    public float betweenmeleesmax;
    public float bulletForce = 10f;
    public float BulletsDamage;
    public float SwordDamage;
    public int EnemyClass;
    //1 - Pistol
    //2 - Katana
    public GameObject Pistol;
    public GameObject Sword;
    public AudioSource PistolSource;
    public AudioClip pistolFireClip;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public bool abletohearbulets;
    public float hear_range;
    Animator animator;
    public GameObject PistolRotater;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform.Find("Skins"))
        {
            if (child.gameObject.activeSelf)
            {
                animator = child.GetComponent<Animator>();
                PistolRotater.transform.SetParent(child.FindDeepChild("Bone.022_end"), false);
            }
        }
        agent = gameObject.GetComponent<NavMeshAgent>();
        sword_animator = Sword.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        BulletsParent = GameObject.FindWithTag("BNAOS").transform;
        if (EnemyClass == 0)
        {
            Debug.Log("Nie przypisa³eœ EnemyClass dzbanie !");
            EnemyClass = 1;
        }

        if (EnemyClass == 1)
        {
            animator.SetBool("isSword", false);
            Pistol.SetActive(true);
            Sword.SetActive(false);
        }
        else if (EnemyClass == 2)
        {
            animator.SetBool("isPistol", false);
            Pistol.SetActive(false);
            Sword.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < rangefromplayerstart && EnemyClass == 1)
        {
            animator.SetBool("isPistol", true);
            agent.destination = player.position;
            if (Vector3.Distance(transform.position, player.position) < rangefromplayerstop)
            {
                agent.isStopped = true;
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                PistolRotater.transform.LookAt(new Vector3(player.position.x, PistolRotater.transform.position.y, player.position.z));

                animator.SetBool("isPistol", false);

                if (!hasshot)
                {
                    PistolRotater.transform.LookAt(new Vector3(player.position.x, PistolRotater.transform.position.y, player.position.z));
                    StartCoroutine(Shoot());


                    hasshot = true;
                }

            }
            else
                agent.isStopped = false;
        }
        else if (Vector3.Distance(transform.position, player.position) < rangefromplayerstart && EnemyClass == 2)
        {
            animator.SetBool("isSword", true);
            agent.destination = player.position;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            if (Vector3.Distance(transform.position, player.position) < melee_stop_range)
            {
                agent.isStopped = true;

                animator.SetBool("isSword", false);
                if (!hasmeleed)
                {
                    StartCoroutine(Melee());
                    hasmeleed = true;
                }

            }
            else
            {
                agent.isStopped = false;
                sword_animator.SetBool("attacking", false);
            }
        }
        else if (EnemyClass != 0&&abletohearbulets)
        {
            bool heardtick = false;
            foreach(GameObject fech in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                if (fech.GetComponent<BulletController>().whocreated == "Player")
                {
                    if (Vector3.Distance(transform.position, fech.transform.position) < hear_range)
                    {
                        heardtick = true;
                    }
                }
            }
            if (heardtick)
            {
                agent.destination = player.position;
                agent.isStopped = false;
            }
        }

        if (Vector3.Distance(gameObject.transform.position, agent.destination) < 1)
        {
            agent.isStopped=true;
        }
    }
    IEnumerator Shoot()
    {
        PistolSource.PlayOneShot(pistolFireClip);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.SetParent(BulletsParent.transform);
        bullet.GetComponent<BulletController>().whocreated = "Enemy";
        bullet.GetComponent<BulletController>().DMG = BulletsDamage;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        yield return new WaitForSeconds(Random.Range(betweenshotsmin, betweenshotsmax));
        hasshot = false;
    }

    IEnumerator Melee()
    {
        sword_animator.SetBool("attacking", true);
        yield return new WaitForSeconds(Random.Range(betweenmeleesmin, betweenmeleesmax));
        sword_animator.SetBool("attacking", false);
        hasmeleed = false;
    }
}
