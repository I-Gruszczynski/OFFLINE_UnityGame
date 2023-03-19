using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionPoster : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    public GameObject KeyButton;
    public GameObject player;

    public Animator animatorPlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                animatorPlayer.SetBool("isPoster", true);
                interactAction.Invoke();
                StartCoroutine(Deletthis());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isInRange = true;
            KeyButton.gameObject.SetActive(true);
            Debug.Log("InRange");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isInRange = false;
            KeyButton.gameObject.SetActive(false);
            Debug.Log("OutRange");
        }
    }

    public void Poster()
    {
        Debug.Log("Emocja nowa");
    }
    IEnumerator Deletthis()
    {
        yield return new WaitForSeconds(animatorPlayer.GetCurrentAnimatorStateInfo(0).length);
        animatorPlayer.SetBool("isPoster", false);
        Destroy(gameObject);
    }
}
