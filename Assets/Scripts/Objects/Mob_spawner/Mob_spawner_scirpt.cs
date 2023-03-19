using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_spawner_scirpt : MonoBehaviour
{
    public GameObject particlesholder;
    public GameObject particleset;
    public GameObject enemyholder;
    public GameObject enemyprefab;
    public GameObject player;
    public float spawndistance;
    public float cooldownmin;
    public float cooldownmax;
    public float placementmin;
    public float placementmax;
    public bool duringspawn = false;
    public Material expmaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < spawndistance)
        {
            if (!duringspawn)
            {
                StartCoroutine(Spawn_enemy());
            }
        }
    }
    IEnumerator Spawn_enemy()
    {
        duringspawn = true;
        float temptime = Random.Range(cooldownmin, cooldownmax);
        yield return new WaitForSeconds(temptime);
         //enemyholder.transform.Find("DropAmmo").GetComponent<MeshRenderer>().material = expmaterial;
        Instantiate(particleset, particlesholder.transform, false);
        float randomx;
        float randomy;
        do
        {
            randomx = Random.Range(placementmin, placementmax);
        }
        while (randomx == 0);
        do
        {
            randomy = Random.Range(placementmin, placementmax);
        }
        while (randomy == 0);
        Vector3 temp1 = new Vector3(transform.position.x + randomx, transform.position.y+1, transform.position.z + randomy);
        GameObject tempenemy = Instantiate(enemyprefab, temp1, transform.rotation, enemyholder.transform);
        int temp2 = Random.Range(1, 3);
        tempenemy.GetComponent<EnemyMovement>().EnemyClass = temp2;
        Transform Skins = tempenemy.transform.Find("Skins");
        for (int i = 0; i < Skins.childCount; i++)
        {
            Skins.GetChild(i).gameObject.SetActive(false);
        }
        Skins.GetChild(Random.Range(0, Skins.childCount)).gameObject.SetActive(true);

        duringspawn = false;
    }
}
