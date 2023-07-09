using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds;
    [SerializeField]
    AudioListener[] listeners;

    private static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else { Destroy(instance); return; }


        DontDestroyOnLoad(instance);

        foreach (var listener in from CustomListener listener in listeners
                                 where listener._AudioType == AudioType.audioListener
                                 select listener)
        {
            listener.listenerInstance = gameObject.AddComponent<AudioListener>();
        }

        foreach (Sound sound in sounds)
        {
            if (sound._AudioType == AudioType.audioSource)
                sound.customAudioSource = gameObject.AddComponent<AudioSource>();

            sound.customAudioSource.name = sound.name;
            sound.customAudioSource.clip = sound.audioClip;
            sound.customAudioSource.outputAudioMixerGroup = sound.audioMixerGroup;
            sound.customAudioSource.mute = sound.mute;
            sound.customAudioSource.playOnAwake = sound.playOnAwake;
            sound.customAudioSource.loop = sound.loop;
            sound.customAudioSource.volume = sound.volume;
            sound.customAudioSource.pitch = sound.pitch;
        }
    }
    /// <summary>
    /// assembly definition should have this refence guid to call this method 
    /// </summary>
    public void SoundPlay()
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogError(string.Format($"sound inserted: {name} not found"));
            return;
        }
    }
}
