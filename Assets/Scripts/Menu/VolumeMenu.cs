using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider volumeMasterSlider;
    [SerializeField] private Slider volumeSFXSlider;
    [SerializeField] private Slider volumeAmbianceSlider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("AmbianceVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume(volumeMasterSlider.value);
            SetAmbianceVolume(volumeAmbianceSlider.value);
            SetSFXVolume(volumeSFXSlider.value);
        }
    }


    public void SetAmbianceVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        mixer.SetFloat("AMBIANCE", volume);
        PlayerPrefs.SetFloat("AmbianceVolume", value);

    }


    public void SetSFXVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        mixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetMasterVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        mixer.SetFloat("MASTER", volume);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }


    public void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        float ambianceVolume = PlayerPrefs.GetFloat("AmbianceVolume");
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");

        volumeMasterSlider.value = masterVolume;
        volumeAmbianceSlider.value = ambianceVolume;
        volumeSFXSlider.value = sfxVolume;

        SetAmbianceVolume(ambianceVolume);
        SetSFXVolume(sfxVolume);
        SetMasterVolume(masterVolume);

        //Debug.Log($"Load volumes : MASTER {masterVolume} | AMBIANCE {ambianceVolume} | SFX {sfxVolume}");
    }
}
