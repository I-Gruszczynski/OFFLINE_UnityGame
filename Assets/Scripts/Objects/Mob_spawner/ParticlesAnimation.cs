using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z + Time.deltaTime);
                child.GetComponent<SpriteRenderer>().color = new Color(child.GetComponent<SpriteRenderer>().color.r, child.GetComponent<SpriteRenderer>().color.g, child.GetComponent<SpriteRenderer>().color.b, child.GetComponent<SpriteRenderer>().color.a - Time.deltaTime);
                if (child.GetComponent<SpriteRenderer>().color.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
