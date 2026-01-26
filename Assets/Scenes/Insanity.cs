using UnityEngine;
using System.Collections;

public class Insanity : MonoBehaviour
{
    private int insanityLvl = 0;
    private float lum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetInsanity();
    }

    // Update is called once per frame
    void Update()
    {
        lum = SetLightLevel(transform.position);

        if (insanityLvl < 0.2f)
            StartCoroutine(UpdateInsanity());

        if (insanityLvl == 4)
        {
            Debug.Log("Death");
        }
    }

    public void IncreaseInsanity()
    {
        if (insanityLvl == 4)
            return;
        else
            insanityLvl++;
    }

    public void DecreaseInsanity()
    {
        if (insanityLvl == 0)
            return;
        else
            insanityLvl--;
    }

    public void ResetInsanity()
    {
        insanityLvl = 0;
    }

    public void ChooseInsanity(int lvl)
    {
        if (lvl < 0)
            lvl = 0;
        else if (lvl > 4)
            lvl = 4;
        insanityLvl = lvl;
    }

    public float SetLightLevel(Vector3 position)
    {
        float lightLevel = 0f;

        foreach (Light light in FindObjectsByType<Light>(FindObjectsSortMode.None))
        {
            if (!light.enabled) continue;

            Vector3 dir = light.transform.position - position;

            if (!Physics.Raycast(position, dir, out RaycastHit hit))
            {
                float distance = dir.magnitude;
                float attenuation = 1f / (distance * distance);

                lightLevel += light.intensity * attenuation;
            }
        }

        return lightLevel;
    }

    IEnumerator UpdateInsanity()
    {
        yield return new WaitForSeconds(5f);

        if (lum < 0.2f)
            DecreaseInsanity();
        else
            IncreaseInsanity();
    }
}