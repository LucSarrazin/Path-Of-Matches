using System;
using UnityEngine;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private bool matches = false;
    private static int safeZoneMatchesCompte = 0;
    private static int safeZoneCompte = 0;
    private bool playerInside = false;

    public static bool IsInSafeZone
    {
        get { return safeZoneCompte > 0; }
    }
    public static bool IsInMatcheZone
    {
        get { return safeZoneMatchesCompte > 0; }
    }

    private void Start()
    {
        PlayerDetector();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the player is in a safe area
        if (other.CompareTag("Player") && !playerInside && !matches)
        {
            playerInside = true;
            safeZoneCompte++;
            //Debug.Log("[SZ SCRIPT] Player Enter Safe Zone");
            GameEvents.OnSafeZoneEnter?.Invoke(); 

        }
        else if (other.CompareTag("Player") && !playerInside && matches)
        {
            playerInside = true;
            safeZoneMatchesCompte++;
            //Debug.Log("[SZ SCRIPT] Player Enter Safe Zone");
            GameEvents.OnSafeZoneEnter?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Check if the player is in a safe area
        if (other.CompareTag("Player") && !playerInside && !matches)
        {
            playerInside = true;
            safeZoneCompte++;
        }
        //Check if the player is in a safe area
        else if (other.CompareTag("Player") && !playerInside && matches)
        {
            playerInside = true;
            safeZoneMatchesCompte++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("EXIT : " + other.name);
        //Check if the player is not in a safe area
        if (other.CompareTag("Player") && playerInside && !matches)
        {
            playerInside = false;
            safeZoneCompte--;
            safeZoneCompte = Mathf.Max(0, safeZoneCompte);

            //Debug.Log("[SZ SCRIPT] Player Exit Safe Zone");

            GameEvents.OnDarkZoneEnter?.Invoke();
        }
        else if (other.CompareTag("Player") && playerInside && matches)
        {
            playerInside = false;
            safeZoneMatchesCompte--;
            safeZoneMatchesCompte = Mathf.Max(0, safeZoneMatchesCompte);

            //Debug.Log("[SZ SCRIPT] Player Exit Safe Zone");

            GameEvents.OnDarkZoneEnter?.Invoke();
        }
    }

    private void OnDestroy()
    {
        if (playerInside && !matches)
        {
            playerInside = false;
            safeZoneCompte--;
        }
        else if (playerInside && matches)
        {
            playerInside = false;
            safeZoneMatchesCompte--;
        }
    }

    private void OnDisable()
    {
        if (playerInside && !matches)
        {
            playerInside = false;
            safeZoneCompte--;
        }
        else if (playerInside && matches)
        {
            playerInside = false;
            safeZoneMatchesCompte--;
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
            if (hit.CompareTag("Player") && !playerInside && !matches)
            {
                playerInside = true;
                safeZoneCompte++;
                break;
            }
            else if (hit.CompareTag("Player") && !playerInside && matches)
            {
                playerInside = true;
                safeZoneMatchesCompte++;
                break;
            }
        }
    }
}

