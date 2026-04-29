using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GammaSettings : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;

    [SerializeField] UnityEngine.UI.Slider gammaSlider;

    void Start()
    {
        volume = FindAnyObjectByType<Volume>();
        volume.profile.TryGet(out colorAdjustments);

        float savedValue = PlayerPrefs.GetFloat("GammaValue", 0f);

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = savedValue;
        }

        if (gammaSlider != null)
        {
            gammaSlider.value = savedValue;
        }
    }

    public void SetGamma()
    {
        PlayerPrefs.SetFloat("GammaValue", gammaSlider.value);
        PlayerPrefs.Save();

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = gammaSlider.value;
        }
    }
}
