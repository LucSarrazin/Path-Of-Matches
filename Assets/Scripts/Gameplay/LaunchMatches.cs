using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchMatches : MonoBehaviour
{
    [Header("Matches Parameters")]
    [SerializeField] private GameObject matches;
    [SerializeField] private float force;
    [SerializeField] private int numberOfMatches;
    [SerializeField] private bool keepInHand;
    [SerializeField] private bool gotMatches = false;
    private bool charging = false;
    private InputSystem_Actions actions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actions =  new InputSystem_Actions();
        actions.Player.Enable();
        actions.Player.Attack.performed += launchPerfomed;
        actions.Player.Attack.canceled += launchCanceled;
    }

    // Update is called once per frame
    void Update()
    {
        if (force > 0 && force < 10)
        {
            if (charging == true)
            {
                force += Time.deltaTime;
            }
            if (force > 1 && charging == false)
            {
                force = 1;
            }
        }

        if (force >= 10 && charging == true)
        {
            force = 10;
        }
        else if (force >= 10 && charging == false)
        {
            force = 1;
        }
    }

    private void launchPerfomed(InputAction.CallbackContext context)
    { 
        if (numberOfMatches > 0)
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

    private void launchCanceled(InputAction.CallbackContext context)
    {
        if (numberOfMatches > 0)
        {
            if (keepInHand == false)
            {
                Launch(force);
                charging = false;
            }
            else
            {
                if (gotMatches == true)
                {
                    gotMatches = false;
                    Launch(force);
                    charging = false;
                }
                else if (gotMatches == false)
                {
                    Debug.Log("Have matches in hand.");
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
        numberOfMatches--;
        GameObject matchesInstantiate = Instantiate(matches, transform.position, new Quaternion(0,0.707106829f,0,0.707106829f));
        Rigidbody rb = matchesInstantiate.GetComponentInChildren<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * forceActual, ForceMode.Impulse);
        }
        Destroy(matchesInstantiate, 30f);
    }
}
