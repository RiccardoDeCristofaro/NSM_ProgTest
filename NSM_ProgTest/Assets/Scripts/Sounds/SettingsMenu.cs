using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderVolume;

    const string MixerMusic = "MasterVolume";
    private void Awake()
    {
        sliderVolume.onValueChanged.AddListener(SetMasterVolume);
    }
    private void Start()
    {    
        if (PlayerPrefs.HasKey(MixerMusic))
            LoadVolume();
        else
            SetMasterVolume(sliderVolume.value);      
    }
     void SetMasterVolume(float volume)
    {       
        audioMixer.SetFloat(MixerMusic, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MixerMusic, volume);
    }
   

    private void LoadVolume()
    {
        sliderVolume.value = PlayerPrefs.GetFloat(MixerMusic);
    }
}
