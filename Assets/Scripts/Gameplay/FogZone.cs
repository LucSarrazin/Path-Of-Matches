using System;
using System.Collections;
using UnityEngine;

public class FogZone : MonoBehaviour
{
    [SerializeField] private int delay;
    [SerializeField] private int insanityAdded;
    [SerializeField] private bool onlyByTorch;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private GameObject cubeCollider;
    private bool oneTime = false;
    private bool destroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !destroyed)
        {
            _playerReferences.PlayerInsanity.addInsanity = insanityAdded;
        }

        if (other.CompareTag("Matches"))
        {
            if (oneTime == false && !destroyed && !onlyByTorch)
            {
                oneTime = true;
                StartCoroutine(DelayWithMatches(other));
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

    public void disableFog()
    {
        if (oneTime == false && !destroyed)
        {
            oneTime = true;
            StartCoroutine(Delay());
        }
    }

    IEnumerator DelayWithMatches(Collider other)
    {
        yield return new WaitForSeconds(delay);
        particleSystem.Stop();
        destroyed = true;
        cubeCollider.SetActive(false);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        particleSystem.Stop();
        destroyed = true;
        cubeCollider.SetActive(false);
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
    }
}
