using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class GraphicsMenu : MonoBehaviour
{
    [Header("[GRAPHICS] GAMMA SETTINGS :")]
    [SerializeField] private List<UnityEngine.UI.Slider> gammaSlider;

    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private float savedValueGamma;

    [Header("[GRAPHICS] SIZE SCREEN SETTINGS :")]
    [SerializeField] private TMP_Dropdown sizeDropDown;

    private int savedValueScreenSizeX;
    private int savedValueScreenSizeY;

    void Start()
    {
        InitialiseGamma();
        InitialiseScreenSize();
    }

    private void InitialiseGamma()
    {
        volume = FindAnyObjectByType<Volume>();
        volume.profile.TryGet(out colorAdjustments);

        if (PlayerPrefs.HasKey("GammaValue"))
        {
            GameObject validationGamma = GameObject.Find("Button_ValidationGamma");
            validationGamma.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }

        savedValueGamma = PlayerPrefs.GetFloat("GammaValue", 0f);

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = savedValueGamma;
        }

        if (gammaSlider != null)
        {
            foreach (UnityEngine.UI.Slider slider in gammaSlider)
            {
                slider.value = savedValueGamma;
            }
        }
    }

    private void InitialiseScreenSize()
    {
        savedValueScreenSizeX = Convert.ToInt32(PlayerPrefs.GetFloat("ScreenSizeValueX", Display.main.systemWidth));
        savedValueScreenSizeY = Convert.ToInt32(PlayerPrefs.GetFloat("ScreenSizeValueY", Display.main.systemHeight));

        if (sizeDropDown != null)
        {
            for (int i = 0; i < sizeDropDown.options.Count; i++)
            {
                if (savedValueScreenSizeX.ToString() + "x" + savedValueScreenSizeY.ToString() == sizeDropDown.options[i].text)
                {
                    sizeDropDown.value = i;
                }
            }
        }
    }

    public void SetGamma(UnityEngine.UI.Slider sliderUse)
    {
        savedValueGamma = sliderUse.value;
        PlayerPrefs.SetFloat("GammaValue", sliderUse.value);
        PlayerPrefs.Save();

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = sliderUse.value;
        }

        foreach (UnityEngine.UI.Slider slider in gammaSlider)
        {
            if (slider.value != savedValueGamma)
            {
                slider.value = savedValueGamma;
            }
        }
    }

    public void SetScreenSize(TMP_Dropdown dropdownUse)
    {
        string size = dropdownUse.options[dropdownUse.value].text;
        string[] parts = size.Split('x');

        if (parts.Length != 2) return;

        int X = int.Parse(parts[0]);
        int Y = int.Parse(parts[1]);

        savedValueScreenSizeX = X;
        savedValueScreenSizeY = Y;
        PlayerPrefs.SetFloat("ScreenSizeValueX", X);
        PlayerPrefs.SetFloat("ScreenSizeValueY", Y);
        PlayerPrefs.Save();

        Screen.SetResolution(savedValueScreenSizeX, savedValueScreenSizeY, FullScreenMode.Windowed);
    }
}


