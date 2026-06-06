using System;
using System.Collections;
using UnityEngine;

public class FogZone : MonoBehaviour
{
    [SerializeField] private int delay;
    [SerializeField] private ParticleSystem particleSystem;
    private bool oneTime = false;
    private bool destroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !destroyed)
        {
            
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
