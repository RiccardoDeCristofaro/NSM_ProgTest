using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Slider sliderSfx;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            LoadVolume();
        else
            SetMusicVolume();

        if (PlayerPrefs.HasKey("SfxVolume"))
            LoadSfx();
        else
            SetSfxVolume();
    }
    public void SetMusicVolume()
    {
        float volume = sliderVolume.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetSfxVolume()
    {
        float Sfxvolume = sliderSfx.value;
        audioMixer.SetFloat("Music", Mathf.Log10(Sfxvolume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", Sfxvolume);
    }

    private void LoadVolume()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("MusicVolume");
    }
    private void LoadSfx()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("MusicVolume");
    }
}
