using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAudioFinishedPlaying : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        Destroy(gameObject, audio.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
