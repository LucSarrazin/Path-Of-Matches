using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private static int safeZoneCompte = 0;
    private bool playerInside = false;

    public static bool IsInSafeZone
    {
        get { return safeZoneCompte > 0; }
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

    private void OnTriggerExit(Collider other)
    {
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
        // Case: the safe zone disappears while the player is inside it
        if (playerInside)
        {
            safeZoneCompte--;
            safeZoneCompte = Mathf.Max(0, safeZoneCompte);
        }
    }
}

