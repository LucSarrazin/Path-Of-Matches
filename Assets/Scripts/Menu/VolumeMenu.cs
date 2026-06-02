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
            SetAmbianceVolume();
            SetSFXVolume();
            SetMasterVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAmbianceVolume()
    {
        float volume = volumeAmbianceSlider.value;
        mixer.SetFloat("AMBIANCE", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("AmbianceVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = volumeSFXSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public void SetMasterVolume()
    {
        float volume = volumeSFXSlider.value;
        mixer.SetFloat("MASTER", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MASTERVolume", volume);
    }

    public void LoadVolume()
    {
        volumeAmbianceSlider.value = PlayerPrefs.GetFloat("AmbianceVolume");
        volumeSFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        volumeMasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        SetAmbianceVolume();
        SetSFXVolume();
        SetMasterVolume();
    }
}
