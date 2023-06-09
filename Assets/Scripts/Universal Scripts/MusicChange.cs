using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    public GameObject music;
    public AudioClip newClip;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            AudioSource audiosource = music.GetComponent<AudioSource>();
            audiosource.clip = newClip;
            audiosource.Play();
        }
    }
}
