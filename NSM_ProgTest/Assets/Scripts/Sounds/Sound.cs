using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    [Header("sound Type")]
    [HideInInspector]
    public AudioSource customAudioSource;
    public AudioType _AudioType;

    [Header("sound Properties")]
    public string name;
    public AudioClip audioClip;
    public AudioMixerGroup audioMixerGroup;
    public bool mute;
    public bool playOnAwake;
    public bool loop;
    [Range(0f, 1f)]
    public float volume = .5f;
    [Range(-3f, 3f)]
    public float pitch = 1f;
    public bool isPlaying;

}
public enum AudioType
{
    AudioSound,
    AudioSfx
}

