using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds;

    private static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject); 


        DontDestroyOnLoad(gameObject);

        SettingCustomSound();
       
    }
    /// <summary>
    /// assembly definition should have this refence guid to call this method 
    /// </summary>
    private void Start()
    {
        SoundPlay("Theme");
    }
    public void SoundPlay(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogError(string.Format($"Sfx inserted: {name} not found"));
            return;
        }
        sound.customAudioSource?.Play();
    }
    public void SfxPlay(string name)
    {
        Sound Sfx = Array.Find(sounds, sound => sound.name == name);
        if (Sfx == null)
        {
            Debug.LogError(string.Format($"Sfx inserted: {name} not found"));
            return;
        }
        Sfx.customAudioSource?.Play();
    }
    private void SettingCustomSound()
    {
        foreach (Sound sound in sounds)
        {
            if (sound._AudioType == AudioType.AudioSound || sound._AudioType == AudioType.AudioSfx)
            {
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
    }
}
