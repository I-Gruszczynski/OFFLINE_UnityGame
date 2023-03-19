using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Robot_Boss_AI : MonoBehaviour
{
    public float health;
    private Transform target;
    public float moveSpeed;
    public float rotationSpeed;
    float distance;
    public float distancetokeep;
    public bool startedfight = false;
    Transform BNAOS;
    public GameObject bulletPrefab;
    public Transform firePointL;
    public Transform firePointR;
    public float bulletForce = 10f;
    public float BulletsDamage;
    public bool inattack = false;
    public int magsize;
    public AudioSource AudioSrc;
    public AudioClip minigunFireClip;
    public float betweenshotsminigun;
    public Transform grenadePoint;
    public float flightTime = 1f;
    public GameObject grenadePrefab;
    public float throwForce = 40f;
    //public int rotationSpeednade; was 5 
    public float nadeDMG;
    public float nadeRadius;
    public GameObject RollL;
    public GameObject RollR;
    public float rollanimationspeed;
    Vector2 RollLValue;
    Vector2 RollRValue;
    public AudioClip nadelaunchsound;
    bool dead = false;
    bool colided = false;
    public int swordDamagefromplayer;
    public GameObject BossBar;
    public Image BossBarFill;
    float starthealth;
    bool rotate = true;
    public float Spinattackspeed;
    public float timebetweenattacks;
    int lastattackwas = -66;
    int rndattck;
    public int spins360s;

    public bool bossTask;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        BNAOS = GameObject.FindWithTag("BNAOS").transform;
        starthealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            distance = Vector3.Distance(target.position, transform.position);
            Debug.DrawLine(target.position, transform.position);

            if (!startedfight)
            {
                if (distance < distancetokeep)
                {
                    startedfight = true;
                    BossBar.SetActive(true);
                }
                else
                {
                    return;
                }
            }

            BossBarFill.fillAmount = health / starthealth;

            //look at player
            if (rotate)
            {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
            }

            //keep distance to target
            if (distance > distancetokeep)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                RollLValue.x -= Time.deltaTime * rollanimationspeed;
                RollLValue.y -= Time.deltaTime * rollanimationspeed;
                RollRValue.x -= Time.deltaTime * rollanimationspeed;
                RollRValue.y -= Time.deltaTime * rollanimationspeed;
                RollL.GetComponent<Renderer>().material.mainTextureOffset = RollLValue;
                RollR.GetComponent<Renderer>().material.mainTextureOffset = RollRValue;
            }
            else if (distance < distancetokeep - 0.1)
            {
                transform.position += transform.forward * (-1) * moveSpeed * Time.deltaTime;
                RollLValue.x += Time.deltaTime * rollanimationspeed;
                RollLValue.y += Time.deltaTime * rollanimationspeed;
                RollRValue.x += Time.deltaTime * rollanimationspeed;
                RollRValue.y += Time.deltaTime * rollanimationspeed;
                RollL.GetComponent<Renderer>().material.mainTextureOffset = RollLValue;
                RollR.GetComponent<Renderer>().material.mainTextureOffset = RollRValue;
            }

            //if (Input.GetKeyDown(KeyCode.Z))
            //{
            //    StartCoroutine(Rotate(2));
            //}

            //shoot
            if (!inattack)
            {
                do
                {
                    rndattck = Random.Range(0, 3);
                }
                while (rndattck == lastattackwas);

                if (rndattck == 0)
                {
                    StartCoroutine(Shoot());
                }
                else if (rndattck == 1)
                {
                    StartCoroutine(Nade());
                }
                else if (rndattck == 2)
                {
                    StartCoroutine(SpinAtack(Spinattackspeed));
                }
                lastattackwas = rndattck;
            }
        }
    }
    IEnumerator Shoot()
    {
        inattack = true;
        for (int i = 0; i < magsize; i++)
        {
            if (!dead)
            {
                GameObject bulletL = Instantiate(bulletPrefab, firePointL.position, firePointL.rotation);
                bulletL.transform.SetParent(BNAOS.transform);
                bulletL.GetComponent<BulletController>().whocreated = "Enemy";
                bulletL.GetComponent<BulletController>().DMG = BulletsDamage;
                Rigidbody rbL = bulletL.GetComponent<Rigidbody>();
                rbL.AddForce(firePointL.forward * bulletForce, ForceMode.Impulse);
                AudioSrc.PlayOneShot(minigunFireClip);

                GameObject bulletR = Instantiate(bulletPrefab, firePointR.position, firePointR.rotation);
                bulletR.transform.SetParent(BNAOS.transform);
                bulletR.GetComponent<BulletController>().whocreated = "Enemy";
                bulletR.GetComponent<BulletController>().DMG = BulletsDamage;
                Rigidbody rbR = bulletR.GetComponent<Rigidbody>();
                rbR.AddForce(firePointR.forward * bulletForce, ForceMode.Impulse);
                AudioSrc.PlayOneShot(minigunFireClip);
                yield return new WaitForSeconds(betweenshotsminigun);
            }
        }
        yield return new WaitForSeconds(timebetweenattacks);
        inattack = false;
    }
    IEnumerator Nade()
    {
        inattack = true;
        //float temp = rotationSpeed;
        //rotationSpeed = rotationSpeednade;
        yield return new WaitForSeconds(1f);
        Vector3 vo = CalculateVelocty(target.position, grenadePoint.position, flightTime);
        GameObject grenade = Instantiate(grenadePrefab, grenadePoint.position, grenadePoint.rotation);
        AudioSrc.PlayOneShot(nadelaunchsound);
        grenade.GetComponent<GranadeScript>().whocreated = "Enemy";
        grenade.GetComponent<GranadeScript>().DMG = nadeDMG;
        grenade.GetComponent<GranadeScript>().radious = nadeRadius;
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(vo * throwForce, ForceMode.VelocityChange);
        grenade.transform.SetParent(BNAOS.transform);
        yield return new WaitForSeconds(timebetweenattacks);
        inattack = false;
    }
    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz / time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
    void DIE()
    {
        bossTask = true;
        dead = true;
        transform.Find("Robot").GetComponent<Animator>().enabled = true;
        transform.Find("DeathExplosion").gameObject.SetActive(true);
        BossBar.SetActive(false);
        Destroy(gameObject, 3);
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Bullet" && other.GetComponent<BulletController>().whocreated == "Player" && !colided)
        {
            colided = true;
            health -= other.GetComponent<BulletController>().DMG;

            if (health <= 0)
            {
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

    IEnumerator SpinAtack(float duration)
    {
        inattack = true;
        rotate = false;
        for (int i = 0; i < spins360s; i++)
        {
            Vector3 startRotation = transform.eulerAngles;
            float endRotation = startRotation.y + 360.0f;
            float t = 0.0f;
            while (t < duration)
            {
                if (!dead)
                {
                    t += Time.deltaTime;
                    float yRotation = Mathf.Lerp(startRotation.y, endRotation, t / duration) % 360.0f;
                    transform.eulerAngles = new Vector3(startRotation.x, yRotation, startRotation.z);

                    GameObject bulletL = Instantiate(bulletPrefab, firePointL.position, firePointL.rotation);
                    bulletL.transform.SetParent(BNAOS.transform);
                    bulletL.GetComponent<BulletController>().whocreated = "Enemy";
                    bulletL.GetComponent<BulletController>().DMG = BulletsDamage;
                    Rigidbody rbL = bulletL.GetComponent<Rigidbody>();
                    rbL.AddForce(firePointL.forward * bulletForce, ForceMode.Impulse);
                    AudioSrc.PlayOneShot(minigunFireClip);

                    GameObject bulletR = Instantiate(bulletPrefab, firePointR.position, firePointR.rotation);
                    bulletR.transform.SetParent(BNAOS.transform);
                    bulletR.GetComponent<BulletController>().whocreated = "Enemy";
                    bulletR.GetComponent<BulletController>().DMG = BulletsDamage;
                    Rigidbody rbR = bulletR.GetComponent<Rigidbody>();
                    rbR.AddForce(firePointR.forward * bulletForce, ForceMode.Impulse);
                    AudioSrc.PlayOneShot(minigunFireClip);
                    yield return null;
                }
                else
                {
                    yield break;
                }
            }
        }
        yield return new WaitForSeconds(timebetweenattacks);
        rotate = true;
        inattack = false;
    }
}