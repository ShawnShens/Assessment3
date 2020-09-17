using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3_Audio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audios;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().clip = audios[0];
        this.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<AudioSource>().isPlaying)
        {
            this.GetComponent<AudioSource>().clip = audios[1];
            this.GetComponent<AudioSource>().Play();
        }
    }
}
