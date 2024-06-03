using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audio;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeAudio(int i)
    {
        source.clip = audio[i];
        source.Play();
    }
}
