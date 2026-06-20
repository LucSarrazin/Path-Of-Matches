using System;
using UnityEngine;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

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
        PlayerDetector();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the player is in a safe area
        if (other.CompareTag("Player") && !playerInside)
        {
            playerInside = true;
            safeZoneCompte++;
            //Debug.Log("[SZ SCRIPT] Player Enter Safe Zone");
            GameEvents.OnSafeZoneEnter?.Invoke(); 

        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Check if the player is in a safe area
        if (other.CompareTag("Player") && !playerInside)
        {
            playerInside = true;
            safeZoneCompte++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("EXIT : " + other.name);
        //Check if the player is not in a safe area
        if (other.CompareTag("Player") && playerInside)
        {
            playerInside = false;
            safeZoneCompte--;
            safeZoneCompte = Mathf.Max(0, safeZoneCompte);

            //Debug.Log("[SZ SCRIPT] Player Exit Safe Zone");

            GameEvents.OnDarkZoneEnter?.Invoke();
        }
    }

    private void OnDestroy()
    {
        if (playerInside)
        {
            playerInside = false;
            safeZoneCompte--;
        }
    }

    private void OnDisable()
    {
        if (playerInside)
        {
            playerInside = false;
            safeZoneCompte--;
        }
    }

    private void PlayerDetector()
    {
        Collider[] hits = Physics.OverlapBox(
            transform.position,
            transform.localScale / 2,
            transform.rotation
        );

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player") && !playerInside)
            {
                playerInside = true;
                safeZoneCompte++;
                break;
            }
        }
    }
}

