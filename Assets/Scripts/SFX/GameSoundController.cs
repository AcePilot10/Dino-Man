using Assets.Scripts.SFX;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameSoundController : MonoBehaviour
{

    private AudioSource _audio;

    private static GameSoundController _instance;
    public static GameSoundController Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    public GameSounds sounds;

    void Awake()
    {
        _instance = this;
        _audio = GetComponent<AudioSource>();
    }

    public void PlaySound(GameSoundTypes sound)
    {
        AudioClip clip = null;
        switch (sound)
        {
            case GameSoundTypes.DIE:
                clip = sounds.dieSound;
                break;
            case GameSoundTypes.CHOMP:
                clip = sounds.chompSound;
                break;
            case GameSoundTypes.LEVEL_UP:
                clip = sounds.levelUpSound;
                break;
            case GameSoundTypes.BEGIN:
                clip = sounds.startSound;
                break;
            case GameSoundTypes.EAT:
                clip = sounds.eatSound;
                break;
        }
        _audio.PlayOneShot(clip);
    }

    public enum GameSoundTypes
    { 
        DIE,
        CHOMP,
        LEVEL_UP,
        BEGIN,
        EAT
    }
}