using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxScript : MonoBehaviour
{
    ShootScript Pistol;
    ShootScript Rifle;
    ShotgunShootScript Shotgun;
    public float show_from_distance;
    public int pistoladd;
    public int rifleadd;
    public int shotgunadd;
    GameObject player;
    public AudioClip Ammo_Pickup_Sound;
    public float Ammo_Pickup_Sound_Volume;
    GameObject keybutton;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Transform Holder = player.transform.FindDeepChild("Holder");
        Pistol = Holder.FindDeepChild("Pistol").Find("Pistol").GetComponent<ShootScript>();
        Rifle = Holder.FindDeepChild("Rifle").Find("Rifle").GetComponent<ShootScript>();
        Shotgun = Holder.FindDeepChild("Shotgun").Find("Shotgun").GetComponent<ShotgunShootScript>();
        keybutton = transform.Find("KeyButton").gameObject;
    }
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= show_from_distance)
        {
            keybutton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pistol.maxAmmo += pistoladd;
                Rifle.maxAmmo += rifleadd;
                Shotgun.maxAmmo += shotgunadd;
                player.GetComponent<AudioSource>().PlayOneShot(Ammo_Pickup_Sound, Ammo_Pickup_Sound_Volume);
                Destroy(gameObject);
            }
        }
        else
        {
            keybutton.SetActive(false);
        }
    }
}
