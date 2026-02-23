using UnityEngine;

public class SoundParameter : MonoBehaviour
{
    private float intensity = 0.006f;

    private AudioSource audioSource; 
    private Insanity player;

    private void Start()
    {
        player = FindAnyObjectByType<Insanity>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.volume = Mathf.Clamp(intensity * player.InsanityLvl, 0f, 1f);
    }
}
