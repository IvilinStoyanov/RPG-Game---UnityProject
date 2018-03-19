using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundOnCollisionEnter : MonoBehaviour
{

    public AudioClip roarClip;
    AudioSource enemyAudio;

    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Mummy Roar");
            enemyAudio.clip = roarClip;
            enemyAudio.Play();
        }
    }
}
