using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvent : MonoBehaviour
{
    public delegate void AnimationEvent();

    public static event AnimationEvent OnSlashAnimationHit;

    void SlashAnimationHitEvent()
    {
        OnSlashAnimationHit();
    }
}


