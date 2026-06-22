using System;
using UnityEngine;

public class TriggerVoiceline : MonoBehaviour
{
    private bool oneTime;
    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private AudioClip voiceClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (oneTime == false)
            {
                oneTime = true;
                voiceSource.PlayOneShot(voiceClip);
            }
        }
    }
}
