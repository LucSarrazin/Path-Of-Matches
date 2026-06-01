using System;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private static int safeZoneCompte = 0;
    private bool playerInside = false;

    public static bool IsInSafeZone
    {
        get { return safeZoneCompte > 0; }
    }
    private void Start()
    {
        Collider[] hits = Physics.OverlapBox(
            transform.position,
            transform.localScale / 2,
            transform.rotation
        );

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                playerInside = true;
                safeZoneCompte++;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the player is in a safe area
        if (other.CompareTag("Player") && !playerInside)
        {
            playerInside = true;
            safeZoneCompte++;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !playerInside)
        {
            playerInside = true;
            safeZoneCompte++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT : " + other.name);
        //Check if the player is not in a safe area
        if (other.CompareTag("Player") && playerInside)
        {
            playerInside = false;
            safeZoneCompte--;
            safeZoneCompte = Mathf.Max(0, safeZoneCompte);
        }
    }

    private void OnDestroy()
    {
        playerInside = false;
    }

    private void OnDisable()
    {
        playerInside = false;
    }
}

