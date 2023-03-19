using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmoothTransitionSaver_script : MonoBehaviour
{
    Vector3 tempplayer;
    Vector3 tempcamera;
    GameObject player;
    GameObject cameraobj;
    Transform Holder;
    bool shotgunenabled;
    int pistolammo;
    int rifleammo;
    int shotgunammo;
    float health;
    int maxhealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraobj = GameObject.FindGameObjectWithTag("MainCamera");
        Holder = player.transform.FindDeepChild("Holder");
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void launchzmianasceny()
    {
        StartCoroutine(zmianasceny());
    }
    IEnumerator zmianasceny()
    {
        tempplayer = player.transform.position;
        tempcamera = cameraobj.transform.position;
        shotgunenabled = player.GetComponent<WeaponChanger>().Shotgun_enabled;
        pistolammo = Holder.FindDeepChild("Pistol").Find("Pistol").GetComponent<ShootScript>().maxAmmo;
        rifleammo = Holder.FindDeepChild("Rifle").Find("Rifle").GetComponent<ShootScript>().maxAmmo;
        shotgunammo = Holder.FindDeepChild("Shotgun").Find("Shotgun").GetComponent<ShotgunShootScript>().maxAmmo;
        health = player.GetComponent<HeartsScript>().HealthAmmount;
        maxhealth = player.GetComponent<HeartsScript>().MaxHealthAmmount;


        SceneManager.LoadScene("P2", LoadSceneMode.Single);
        while (SceneManager.GetActiveScene().name != "P2")
        {
            yield return null;
        }
        if (SceneManager.GetActiveScene().name == "P2")
        {
            //Player Position
            GameObject Playernew = GameObject.FindGameObjectWithTag("Player");
            Playernew.GetComponent<CharacterController>().enabled = false;
            Playernew.transform.position = tempplayer;
            Playernew.GetComponent<CharacterController>().enabled = true;
            Debug.Log("Scena zmieniona do: " + SceneManager.GetActiveScene().name);

            //Camera
            GameObject Cameranew = GameObject.FindGameObjectWithTag("MainCamera");
            Cameranew.GetComponent<CameraFollow>().enabled = false;
            Cameranew.transform.position = tempcamera;
            Cameranew.GetComponent<CameraFollow>().enabled = true;
            
            //shotgun_enabled
            Playernew.GetComponent<WeaponChanger>().Shotgun_enabled = shotgunenabled;
            Holder = Playernew.transform.FindDeepChild("Holder");

            //ammo
            Holder.FindDeepChild("Pistol").Find("Pistol").GetComponent<ShootScript>().maxAmmo=pistolammo;
            Holder.FindDeepChild("Rifle").Find("Rifle").GetComponent<ShootScript>().maxAmmo=rifleammo;
            Holder.FindDeepChild("Shotgun").Find("Shotgun").GetComponent<ShotgunShootScript>().maxAmmo=shotgunammo;

            //health
            Playernew.GetComponent<HeartsScript>().HealthAmmount=health;
            Playernew.GetComponent<HeartsScript>().MaxHealthAmmount = maxhealth;
        }
    }
}
