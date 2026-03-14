using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLaunchMatches : MonoBehaviour
{
    [Header("Matches Parameters")]
    public GameObject matches;
    [SerializeField] private float force;
    [SerializeField] private float timeForce;
    [SerializeField] private int numberOfMatches;

    [SerializeField] private bool keepInHand;
    [SerializeField] private bool gotMatches = false;
    [SerializeField] private GameObject handMatches;

    private bool charging = false;

    /* --- Public references to Update in UI --- */

    public GameObject Matches
    {
        get => matches;
        set
        {
            if (matches == value) return;
            matches = value;
            OnChangeMatches?.Invoke(matches);
        }
    }

    public int NumberOfMatches
    {
        get => numberOfMatches;
        set
        {
            if (numberOfMatches == value) return;
            numberOfMatches = value;
            OnChangeNumberOfMatches?.Invoke(numberOfMatches);
        }
    }

    public float Force
    {
        get => force;
        set { if (force == value) return; /* Maybe add here a constraint condition for fluidity ( < 0.1 ?) */
            force = value; 
            OnForceChange?.Invoke(force);
        }
    }

    /* --- Events --- */
    public Action<int> OnChangeNumberOfMatches;
    public Action<float> OnForceChange;
    public Action<GameObject> OnChangeMatches;

    /* --- Update : force --- */
    void Update()
    {
        if (Force > 0 && Force < 10)
        {
            if (charging == true)
            {
                Force += Time.deltaTime * timeForce;
            }
            if (Force > 1 && charging == false)
            {
                Force = 1;
            }
        }

        if (Force >= 10 && charging == true)
        {
            Force = 10;
        }
        else if (Force >= 10 && charging == false)
        {
            Force = 1;
        }
    }

    /* --- Public methods to call in the State Machine --- */

    public void StartThrowCharge() => launchPerformed(); 
    public void StopThrowCharge() => launchCanceled();


    private void launchPerformed()
    {
        if (NumberOfMatches > 0 && matches != null)
        {
            if (keepInHand == false)
            {
                charging = true;
            }
            else
            {
                if (gotMatches == true)
                {
                    charging = true;
                }
            }
        }
        else
        {
            Debug.Log("No matches left.");
        }
    }

    private void launchCanceled()
    {
        if (NumberOfMatches > 0 && matches != null)
        {
            if (keepInHand == false)
            {
                Launch(Force);
                charging = false;
            }
            else
            {
                if (gotMatches == true)
                {
                    handMatches.SetActive(false);
                    gotMatches = false;
                    Launch(Force);
                    charging = false;
                }
                else if (gotMatches == false)
                {
                    Debug.Log("Have matches in hand.");
                    handMatches.SetActive(true);
                    gotMatches = true;
                }
            }
        }
        else
        {
            Debug.Log("No matches left.");
        }
    }

    void Launch(float forceActual)
    {
        Debug.Log("Launching matches.");
        NumberOfMatches--;
        GameObject matchesInstantiate = Instantiate(matches, transform.position, new Quaternion(0, 0.707106829f, 0, 0.707106829f));
        Rigidbody rb = matchesInstantiate.GetComponentInChildren<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * forceActual, ForceMode.Impulse);
        }
        Destroy(matchesInstantiate, 15f);
    }
}
