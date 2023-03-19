using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;

public class ShotgunShootScript : MonoBehaviour
{
    public GameObject shotgun;

    List<Quaternion> pellets;
    public int bulletNumber;
    public int pelletCount;
    public float spreadAngle;
    public float bulletForce;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float Maxspread;

    AudioSource shotgunSource;
    public AudioClip shotgunFireClip;
    public AudioClip shotgunReloadClip;
    public AudioClip shotgunReloadClipP1;
    public AudioClip shotgunReloadClipP2;

    public float fireRate = 2f;
    public float timeBetweenShots = 0f;
    private float shotCounter;

    private bool isReloading = false;
    public int maxAmmo = 60;
    public int magazineSize = 30;
    public float reloadTime = 2f;
    public float DMG;
    int bulletLost;

    public ParticleSystem ParticleSystem;
    public GameObject UIobj;
    public GameObject Reload_Indicator_Prefab;
    public GameObject BulletsParent;
    GameObject Reload_Indicator;
    public bool waschanged = false;
    public Text AmmoText;
    public GamePauseScript GamePauseScript;

    public GameObject player;

    public Animator animatorGun;
    // Start is called before the first frame update
    void Awake()
    {
       // pellets = new List<Quaternion>(pelletCount);
       // for (int i = 0; i < pelletCount; i++)
      //  {
      //      pellets.Add(Quaternion.Euler(Vector3.zero));
      //  }
    }

    void Start()
    {
        shotgunSource = shotgun.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waschanged && isReloading)
        {
            StartCoroutine(Reload());
            waschanged = false;
        }
        else
            waschanged = false;



        if (!isReloading)
        {

            if (bulletNumber <= 0 && maxAmmo > 0 || (bulletNumber != magazineSize && (Input.GetKeyDown(KeyCode.R)&&!GamePauseScript.ispaused)))
            {
                maxAmmo = maxAmmo - bulletLost;
                bulletLost = 0;
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetMouseButtonDown(0)&&!GamePauseScript.ispaused)
            {
                fire();
            }
        }
        if (bulletNumber > maxAmmo)
        {
            bulletNumber = maxAmmo;
        }

        if (bulletNumber >= 0 || maxAmmo >= 0)
        {
            AmmoText.text = bulletNumber.ToString() + "/" + maxAmmo.ToString();
        }
        else
        {
            AmmoText.text = "0/0";
        }

    }

    void fire()
    {
        timeBetweenShots = Time.time + 1f / fireRate;
        if (bulletNumber > 0)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                //   int i = -1;

                //        foreach (Quaternion quat in pellets)
                //    {
                //      i++;
                // pellets[i] = Random.rotation;

                for (int i = 0; i < pelletCount; i++)
                {
                    var proj = Quaternion.Euler(Vector3.zero);
                    //  do
                    //   {
                    proj = Random.rotation;
                    //}
                    /*while (
                             proj.w > Maxspread
                             || proj.w < (Maxspread * -1)
                             || proj.x > Maxspread 
                             || proj.x < (Maxspread * -1)
                        || proj.y > Maxspread 
                        || proj.y < (Maxspread * -1)
                        //|| proj.z > Maxspread 
                        || proj.z < (Maxspread * -1)
                        );

                    proj.w = 0.1F;
                    proj.x = 0.1F;
                    proj.y = 0.1F;
                    proj.z = 0.1F;
                    Debug.Log("W" + proj.w);
                    Debug.Log("X" + proj.x);
                    Debug.Log("Y" + proj.y);
                    Debug.Log("Z" + proj.z);
                   */
                    //Debug.Log(proj.ToString());
                    shotgunSource.PlayOneShot(shotgunFireClip);
                    ParticleSystem.Play();
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    bullet.transform.SetParent(BulletsParent.transform);
                    bullet.GetComponent<BulletController>().whocreated = "Player";
                    bullet.GetComponent<BulletController>().DMG = DMG;
                    bullet.transform.rotation = Quaternion.RotateTowards(bullet.transform.rotation, proj, spreadAngle);
                    bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletForce);
                }
            }
        }
        bulletNumber--;
        bulletLost++;
        //}
    }

    IEnumerator Reload()
    {
        Animator animator = player.GetComponent<Animator>();

        animator.SetBool("isReload", true);
        animatorGun.enabled = true;
        animatorGun.PlayInFixedTime("Shotgun_reload", -1, 0);

        shotgunSource.PlayOneShot(shotgunReloadClipP1);
        isReloading = true;
        Reload_Indicator = Instantiate(Reload_Indicator_Prefab);
        Reload_Indicator.GetComponent<Reload_Indicator_Script>().time = reloadTime;
        Reload_Indicator.transform.SetParent(UIobj.transform, false);
        yield return new WaitForSeconds(reloadTime);
        shotgunSource.PlayOneShot(shotgunReloadClipP2);
        bulletNumber = magazineSize;
        //maxAmmo = maxAmmo - magazineSize;
        isReloading = false;
        animator.SetBool("isReload", false);
        animatorGun.enabled = false;
        timeBetweenShots = 0;
    }
}
