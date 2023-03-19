using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLvl_V1 : MonoBehaviour
{
    public GameObject transition;
    public GameObject player;
    Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            StartCoroutine(zmianasceny());
            //GameObject.FindGameObjectWithTag("Player").transform.rotation = temp.rotation;
            //GameObject.FindGameObjectWithTag("Player").transform.eulerAngles = temp.eulerAngles;

        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            SceneManager.UnloadSceneAsync("P1");
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Debug.Log(SceneManager.GetActiveScene().name);
        }


        IEnumerator zmianasceny()
        {
            temp = player.transform.position;
            SceneManager.LoadScene("P2", LoadSceneMode.Single);
            while (SceneManager.GetActiveScene().name != "P2")
            {
                yield return null;
            }
            if (SceneManager.GetActiveScene().name == "P2")
            {
                GameObject Playernew = GameObject.FindGameObjectWithTag("Player");
                Debug.Log(Playernew.name);
                Playernew.GetComponent<CharacterController>().enabled = false;
                Playernew.transform.position = temp;
                Playernew.GetComponent<CharacterController>().enabled = true;
                Debug.Log(Playernew.name);
            }
        }
    }
}
