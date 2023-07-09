using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            LoadVolume();
        else
            SetMusicVolume();
    }
    public void SetMusicVolume()
    {
        float volume = sliderVolume.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    private void LoadVolume()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("MusicVolume");
    }
}
