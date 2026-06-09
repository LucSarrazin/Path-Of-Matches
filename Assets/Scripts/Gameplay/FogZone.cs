using System;
using System.Collections;
using UnityEngine;

public class FogZone : MonoBehaviour
{
    [SerializeField] private int delay;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private PlayerReferences _playerReferences;
    private bool oneTime = false;
    private bool destroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !destroyed)
        {
            _playerReferences.PlayerInsanity.addInsanity = 20;
        }

        if (other.CompareTag("Matches"))
        {
            if (oneTime == false && !destroyed)
            {
                oneTime = true;
                StartCoroutine(Delay(other));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !destroyed)
        {
            _playerReferences.PlayerInsanity.addInsanity = 8;
        }

        if (other.CompareTag("Matches"))
        {
            
        }
    }

    IEnumerator Delay(Collider other)
    {
        yield return new WaitForSeconds(delay);
        particleSystem.Stop();
        destroyed = true;
    }
}
