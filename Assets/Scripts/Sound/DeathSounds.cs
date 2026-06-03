using UnityEngine;

public class DeathSounds : MonoBehaviour
{
    [Header("[SETTINGS]")]
    [SerializeField] private float _volume = 0.5f;
    [SerializeField] private AudioClip audioWakeUp;

    private AudioSource audioSource;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        if (audioWakeUp != null && Insanity.isDead)
        {
            audioSource.PlayOneShot(audioWakeUp, _volume);
            Insanity.isDead = false;
        }
    }
}
