using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GammaSettings : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private float savedValue;

    [SerializeField] List<UnityEngine.UI.Slider> gammaSlider;

    void Start()
    {
        volume = FindAnyObjectByType<Volume>();
        volume.profile.TryGet(out colorAdjustments);

        if (PlayerPrefs.HasKey("GammaValue"))
        {
            GameObject validationGamma = GameObject.Find("Button_ValidationGamma"); 
            validationGamma.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }

        savedValue = PlayerPrefs.GetFloat("GammaValue", 0f);

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = savedValue;
        }

        if (gammaSlider != null)
        {
            foreach (UnityEngine.UI.Slider slider in gammaSlider)
            {
                slider.value = savedValue;
            }
        }
    }

    public void SetGamma(UnityEngine.UI.Slider sliderUse)
    {
        savedValue = sliderUse.value;
        PlayerPrefs.SetFloat("GammaValue", sliderUse.value);
        PlayerPrefs.Save();

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = sliderUse.value;
        }

        foreach (UnityEngine.UI.Slider slider in gammaSlider)
        {
            if (slider.value != savedValue)
            {
                slider.value = savedValue;
            }
        }
    }
}


