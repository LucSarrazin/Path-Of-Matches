using System;
using System.Collections;
using UnityEngine;

public class FogZone : MonoBehaviour
{
    [SerializeField] private int delay;
    [SerializeField] private MeshRenderer meshRenderer;
    private bool oneTime = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }

        if (other.CompareTag("Matches"))
        {
            if (oneTime == false)
            {
                oneTime = true;
                Destroy(other.gameObject, delay);
                StartCoroutine(Delay());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }

        if (other.CompareTag("Matches"))
        {
            
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        meshRenderer.enabled = false;
    }
}
