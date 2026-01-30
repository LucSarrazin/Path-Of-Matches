using UnityEngine;

public class DarkZone : MonoBehaviour
{
    private static bool isInDarkZone;

    public bool IsInDarkZone
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check if the player is not in a dark area
        if (other.CompareTag("Player"))
        {
            IsInDarkZone = false;
        }
    }
}

