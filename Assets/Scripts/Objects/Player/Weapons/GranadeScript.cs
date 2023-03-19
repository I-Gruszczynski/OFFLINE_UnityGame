using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeScript : MonoBehaviour
{
    public float delay = 3f;
    public float radious = 5f;
    public float force = 700f;
    public float DMG;
    //public GameObject enemy;
    float countDown;
    Rigidbody rb;
    public GameObject AudioPrefab;
    GameObject killcounter;
    public GameObject explosioneffect;
    public string whocreated;
    // Start is called before the first frame update
    public void Start()
    {
        if (whocreated == "Enemy")
        {
            gameObject.layer = LayerMask.NameToLayer("EnemyProjectiles");
        }
        killcounter = GameObject.FindGameObjectsWithTag("killcounter")[0];
        countDown = delay;
        //EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (whocreated == "Enemy")
        {
            return;
        }
        else
        {
            countDown -= Time.deltaTime;

            if (countDown <= 0)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, radious);
                foreach (Collider nearbyObject in colliders)
                {

                    if (nearbyObject.tag == "Enemy")
                    {
                        Animator Eanimator = nearbyObject.gameObject.GetComponentInChildren<Animator>();
                        nearbyObject.gameObject.GetComponent<EnemyHealth>().health -= DMG;
                        if (nearbyObject.gameObject.GetComponent<EnemyHealth>().health <= 0)
                        {
                            killcounter.GetComponent<killcounterscript>().killcount++;
                            Eanimator.SetBool("isDead", true);
                            Destroy(nearbyObject.gameObject,4);
                        }
                        Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                    }
                }
                GameObject soundinst = Instantiate(AudioPrefab, transform.position, transform.rotation);
                soundinst.transform.SetParent(gameObject.transform.parent, true);
                GameObject exploinst = Instantiate(explosioneffect, transform.position, new Quaternion(0f, 0f, 0f, 0f));
                exploinst.transform.SetParent(gameObject.transform.parent, true);
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radious);
                }
                Destroy(gameObject);
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (whocreated != "Enemy")
        {
            return;
        }
        else
        {
            bool todestroy = false;
            GameObject soundinst = Instantiate(AudioPrefab, transform.position, transform.rotation);
            soundinst.transform.SetParent(gameObject.transform.parent, true);
            GameObject exploinst = Instantiate(explosioneffect, transform.position, new Quaternion(0f, 0f, 0f, 0f));
            exploinst.transform.SetParent(gameObject.transform.parent, true);
            Collider[] colliders = Physics.OverlapSphere(transform.position, radious);
            foreach (Collider nearbyObject in colliders)
            {
                if (nearbyObject.tag == "Player"&&!todestroy)
                {
                    todestroy = true;
                    nearbyObject.gameObject.GetComponent<HeartsScript>().HealthAmmount -= DMG;
                    Destroy(gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
