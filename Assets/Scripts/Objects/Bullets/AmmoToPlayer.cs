using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoToPlayer : MonoBehaviour
{
    GameObject player;
    GameObject pistol;
    GameObject rifle;
    GameObject shotgun;
    public AudioClip Ammo_Pickup_Sound;
    public float Ammo_Pickup_Sound_Volume;
    public GameObject ammo;
    public float speed = 2f;
    public float speedbounce;
    public bool move_to_player_instead_of_bouncing = false;
    Vector3 bouncestartPosition;

    private void Start()
    {
        player  = GameObject.FindGameObjectWithTag("Player");
        pistol  = GameObject.FindGameObjectWithTag("Holder").transform.Find("Pistol").Find("Pistol").gameObject;
        rifle   = GameObject.FindGameObjectWithTag("Holder").transform.Find("Rifle").Find("Rifle").gameObject;
        shotgun = GameObject.FindGameObjectWithTag("Holder").transform.Find("Shotgun").Find("Shotgun").gameObject;
        bouncestartPosition = transform.position;
        bouncestartPosition.y+=0.1f;


        if (!move_to_player_instead_of_bouncing)
        {
            StartCoroutine(Bounce());
        }


    }
    private void LateUpdate()
    {
        if (move_to_player_instead_of_bouncing)
        {
            Vector3 positionPlayer = player.transform.position;
            Vector3 smoothPositionAmmo = Vector3.Lerp(transform.position, positionPlayer, speed);
            ammo.transform.position = smoothPositionAmmo;
            StopCoroutine("Bounce");
        }
        else
        {
            transform.transform.Rotate(0, 1f, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<AudioSource>().PlayOneShot(Ammo_Pickup_Sound, Ammo_Pickup_Sound_Volume);
            Destroy(gameObject);

            if (pistol.activeSelf == true)
            {
                ShootScript shootScript = pistol.GetComponent<ShootScript>();
                shootScript.maxAmmo = shootScript.maxAmmo + 10;
            }
           if (rifle.activeSelf == true)
            {
                ShootScript shootScript = rifle.GetComponent<ShootScript>();
                shootScript.maxAmmo = shootScript.maxAmmo + 10;
            }
            if (shotgun.activeSelf == true)
            {
                ShotgunShootScript shootScript = shotgun.GetComponent<ShotgunShootScript>();
                shootScript.maxAmmo = shootScript.maxAmmo + 10;
            }
           
         }
    }
    IEnumerator Bounce()
    {
        while (transform.position.y < (bouncestartPosition.y + 1))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speedbounce * Time.deltaTime, transform.position.z);
            yield return null;
        }
        while (transform.position.y > (bouncestartPosition.y))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speedbounce * Time.deltaTime, transform.position.z);
            yield return null;
        }
        StartCoroutine(Bounce());
    }
}
