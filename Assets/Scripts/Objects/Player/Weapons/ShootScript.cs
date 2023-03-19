using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;

public class ShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject rifle;
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject player;

    AudioSource PistolSource;
    AudioSource RifleSource;
    public AudioClip pistolFireClip;
    public AudioClip pistolReloadClipP1;
    public AudioClip pistolReloadClipP2;
    public AudioClip rifleFireClip;
    public AudioClip rifleReloadClipP1;
    public AudioClip rifleReloadClipP2;

    public int bulletNumber = 10;
    public float bulletForce = 10f;
    public float fireRate = 15f;
    public Transform firePoint;

    public float timeBetweenShots = 0f;
    private float shotCounter;

    private bool isReloading = false;
    public int maxAmmo = 60;
    public int magazineSize = 30;
    public float pistolreloadTime = 2f;
    public float riflereloadTime = 2f;
    public float DMG;
    float reloadTime;
    int bulletLost;

    public bool waschanged = false ;
    public ParticleSystem ParticleSystem;
    public GameObject UIobj;
    public GameObject Reload_Indicator_Prefab;
    public GameObject BulletsParent;
    GameObject Reload_Indicator;
    public Text AmmoText;
    public GamePauseScript GamePauseScript;
    // Start is called before the first frame update

    public float x;
    public float z;

    void Start()
    {
        RifleSource = rifle.GetComponent<AudioSource>();
        PistolSource = pistol.GetComponent<AudioSource>();
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
            if ((bulletNumber <= 0 && maxAmmo > 0 || (bulletNumber!=magazineSize&&Input.GetKeyDown(KeyCode.R)))&&!GamePauseScript.ispaused)
            {
                maxAmmo = maxAmmo - bulletLost;
                bulletLost = 0;
                StartCoroutine(Reload());
                return;
            }
 
            if (Input.GetMouseButtonDown(0) && Time.time >= timeBetweenShots && !GamePauseScript.ispaused)
            {
                timeBetweenShots = Time.time + 1f / fireRate;
                if (bulletNumber > 0)
                {
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        if (rifle.activeSelf)
                        {
                            RifleSource.PlayOneShot(rifleFireClip);
                        }
                        else if (pistol.activeSelf)
                        {
                            PistolSource.PlayOneShot(pistolFireClip);
                        }

                        Vector3 worldPosition;
                        Plane plane = new Plane(Vector3.up, 0);
                        float distance;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (plane.Raycast(ray, out distance))
                        {
                            worldPosition = ray.GetPoint(distance);
                            firePoint.LookAt(new Vector3(worldPosition.x, firePoint.position.y, worldPosition.z));
                        }
                        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                        bullet.transform.SetParent(BulletsParent.transform);
                        bullet.GetComponent<BulletController>().whocreated = "Player";
                        bullet.GetComponent<BulletController>().DMG = DMG;
                        Rigidbody rb = bullet.GetComponent<Rigidbody>();
                        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
                        ParticleSystem.Play();
                        bulletNumber--;
                        bulletLost++;
                    }
                }
            }

            if (Input.GetMouseButton(0) && Time.time >= timeBetweenShots && rifle.activeSelf == true && !GamePauseScript.ispaused)
            {
                timeBetweenShots = Time.time + 1f / fireRate;
                if (bulletNumber > 0)
                {
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        RifleSource.PlayOneShot(rifleFireClip);
                        Vector3 worldPosition;
                        Plane plane = new Plane(Vector3.up, 0);
                        float distance;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (plane.Raycast(ray, out distance))
                        {
                            worldPosition = ray.GetPoint(distance);
                            firePoint.LookAt(new Vector3(worldPosition.x, firePoint.position.y, worldPosition.z));
                        }
                        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                        bullet.transform.SetParent(BulletsParent.transform);
                        bullet.GetComponent<BulletController>().whocreated = "Player";
                        bullet.GetComponent<BulletController>().DMG = DMG;
                        Rigidbody rb = bullet.GetComponent<Rigidbody>();
                        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
                        ParticleSystem.Play();
                        bulletNumber--;
                        bulletLost++;
                    }
                }
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

    IEnumerator Reload()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        Animator animator = player.GetComponent<Animator>();

        animator.SetBool("isReload", true);
        if (x != 0 || z != 0)
        {
            animator.SetBool("isReload_run", true);
        }
        else
        {
            animator.SetBool("isReload_run", false);
        }

        if (rifle.activeSelf)
        {
            reloadTime = riflereloadTime;
            RifleSource.PlayOneShot(rifleReloadClipP1);
        }
        else if (pistol.activeSelf)
        {
            reloadTime = pistolreloadTime;
            PistolSource.PlayOneShot(pistolReloadClipP1);
        }

        isReloading = true;
        Reload_Indicator = Instantiate(Reload_Indicator_Prefab);
        Reload_Indicator.GetComponent<Reload_Indicator_Script>().time=reloadTime;
        Reload_Indicator.transform.SetParent(UIobj.transform,false);
        yield return new WaitForSeconds(reloadTime);
        if (rifle.activeSelf){RifleSource.PlayOneShot(rifleReloadClipP2);}
        else if (pistol.activeSelf){PistolSource.PlayOneShot(pistolReloadClipP2);}
        bulletNumber = magazineSize;
        //maxAmmo = maxAmmo - magazineSize;
        isReloading = false;
        animator.SetBool("isReload", false);
        timeBetweenShots = 0;
    }
}
