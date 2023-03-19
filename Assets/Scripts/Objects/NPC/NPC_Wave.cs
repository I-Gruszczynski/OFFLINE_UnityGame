using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Wave : MonoBehaviour
{
    public GameObject player;
    public GameObject DialogueManager;
    Animator animator;
    bool look = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if(look)
        {
            gameObject.transform.LookAt(player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        look = true;
        if (other.gameObject == player)
        {
            animator.SetBool("isWaving", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            animator.SetBool("isWaving", false);
        }
    }
}
