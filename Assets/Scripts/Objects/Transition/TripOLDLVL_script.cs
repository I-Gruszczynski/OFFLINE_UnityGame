using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripOLDLVL_script : MonoBehaviour
{
    GameObject player;
    public GameObject GateOLDLVL;
    public SmoothTransitionSaver_script smoothTransitionSaver_Script;
    bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player&&!done)
        {
            done = true;
            GateOLDLVL.SetActive(true);
            smoothTransitionSaver_Script.launchzmianasceny();
        }
    }
}
