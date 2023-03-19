using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string whocreated;
    public Color EnemyBulletColor;
    public GameObject Hiteffect;
    public float DMG;
    Transform BNAOS;
    private void Start()
    {
        BNAOS = GameObject.FindWithTag("BNAOS").transform;
        if (whocreated == "Enemy")
        {
            gameObject.GetComponent<MeshRenderer>().material.color = EnemyBulletColor;
            gameObject.layer = LayerMask.NameToLayer("EnemyProjectiles");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag != "Bullet")
        {
            GameObject temp = Instantiate(Hiteffect, transform.position, new Quaternion(0,0,0,0));
            temp.transform.SetParent(BNAOS,true);
            Destroy(temp,0.1f);
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        Destroy(gameObject, 5);
    }
}
