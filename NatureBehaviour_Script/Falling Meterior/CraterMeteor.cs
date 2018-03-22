using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterMeteor : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip meteorSplash;

    // Use this for initialization
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(meteorSplash);
    }
}
