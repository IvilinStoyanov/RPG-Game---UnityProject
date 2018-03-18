using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAudio_Script : MonoBehaviour
{


    public AudioClip[] audioClip;

    public AudioSource source;

    void Attack1()
    {
        source.pitch = 1;
        source.PlayOneShot(audioClip[0]);
    }

    void Attack2()
    {
        source.pitch = 1;
        source.PlayOneShot(audioClip[1]);
    }

    void Attack3()
    {
        source.pitch = 1;
        source.PlayOneShot(audioClip[2]);
    }

    void Attack4()
    {
        source.pitch = 2;
        source.PlayOneShot(audioClip[3]);
    }
}
