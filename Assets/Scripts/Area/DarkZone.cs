using UnityEngine;

public class DarkZone : MonoBehaviour
{
    private static bool isInDarkZone;

    public static bool IsInDarkZone
    {
        get { return isInDarkZone; }      
        private set { isInDarkZone = value; } 
    }

    private void Start()
    {
        IsInDarkZone = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the player is in a dark area
        if (other.CompareTag("Player"))
        {
            IsInDarkZone = true;
            GameEvents.OnDarkZoneEnter?.Invoke();
            //Debug.Log("[DZ SCRIPT] Player Enter Dark Zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check if the player is not in a dark area
        if (other.CompareTag("Player"))
        {
            IsInDarkZone = false;
            //Debug.Log("[DZ SCRIPT] Player Exit Dark Zone");
        }
    }
}

