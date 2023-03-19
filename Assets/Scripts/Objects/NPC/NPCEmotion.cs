using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCEmotion : MonoBehaviour
{
    GameObject player;
    public GameObject pistol;
    public GameObject rifle;
    public GameObject shotgun;
    GameObject playerModel;
    public VigneteScript VigneteScript;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerModel = GameObject.FindGameObjectWithTag("main_character");
        animator = playerModel.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<HeartsScript>().regen = true;
            animator.speed = 1.8f;
            VigneteScript.StartCoroutine(VigneteScript.ONanimation());

            pistol.GetComponent<ShootScript>().timeBetweenShots = 0f;
            pistol.GetComponent<ShootScript>().pistolreloadTime = 0.5f;
            rifle.GetComponent<ShootScript>().timeBetweenShots = 0f;
            rifle.GetComponent<ShootScript>().riflereloadTime = 0.5f;
            shotgun.GetComponent<ShotgunShootScript>().timeBetweenShots = 0f;
            shotgun.GetComponent<ShotgunShootScript>().reloadTime = 0.5f;
            //Debug.Log("InRange");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<HeartsScript>().regen = false;
            animator.speed = 1f;
            VigneteScript.StartCoroutine(VigneteScript.OFFanimation());
            VigneteScript.StartCoroutine(VigneteScript.alphafade());

            pistol.GetComponent<ShootScript>().timeBetweenShots = 2f;
            pistol.GetComponent<ShootScript>().pistolreloadTime = 2f;
            rifle.GetComponent<ShootScript>().timeBetweenShots = 0f;
            rifle.GetComponent<ShootScript>().riflereloadTime = 2f;
            shotgun.GetComponent<ShotgunShootScript>().timeBetweenShots = 3f;
            shotgun.GetComponent<ShotgunShootScript>().reloadTime = 2f;
            //Debug.Log("OutRange");
        }
    }
}
