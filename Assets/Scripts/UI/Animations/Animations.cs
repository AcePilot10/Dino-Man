using Assets.Scripts.UI.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public AnimationBase[] animations;

    void Start()
    {
        PlayAnimations();
    }

    public void PlayAnimations()
    {
        foreach (var animation in animations)
        {
            animation.rect.Move(animation.destination, animation.animationDuration).SetEase(animation.ease);
        }
    }
}