using System;
using System.Collections;
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
    [SerializeField] private Watch leftHand;
    [SerializeField] private float timeBeforeDisable = 12f;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private ShakeCamera cameraShake;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip throwMatches;
    [SerializeField] private AudioClip takeMatches;
    [SerializeField] private AudioClip matchesFire;
    [SerializeField] private AudioClip burningFinger;
    [SerializeField] private bool oneTimeSound = false;
    [SerializeField] private bool canThrow = true;

    [SerializeField] private float timeBeforeEndAnimation = 4f; 
    private bool _autoReleased = false; // flag : allumette d�j� rel�ch�e automatiquement
    public bool AutoReleased => _autoReleased;

    public Watch LeftHand => leftHand;

    public bool CanThrow => canThrow;

    // Et une m�thode pour le reset proprement depuis l'ext�rieur
    public void ConsumeAutoRelease()
    {
        _autoReleased = false;
        Force = 1;
    }

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
    void FixedUpdate()
    {
        if (charging)
        {
            handAnimator.Play("Cooldown", 0, 0f);
        }

        if (Force > 0 && Force < 10)
        {
            if (charging == true)
            {
                cameraShake.ShakeScreenMatches();
                Force += Time.deltaTime * timeForce;
                //handAnimator.SetBool("Throw", false);
                //Debug.Log("Bool Throw = false force 1 (Charging = true)");
            }
            if (Force > 1 && charging == false)
            {
                Force = 1;
                //handAnimator.SetBool("Throw", false);
                //Debug.Log("Bool Throw = false force 1 (Charging = false)");
            }
        }

        if (Force >= 10 && charging == true)
        {
            Force = 10;
            //handAnimator.SetBool("Throw", false);
            //Debug.Log("Bool Throw = false force 10 (Charging = true)");
        }
        else if (Force >= 10 && charging == false)
        {
            Force = 1;
            //handAnimator.SetBool("Throw", false);
            //Debug.Log("Bool Throw = false force 10 (Charging = false)");
        }

        if (gotMatches)
        {
            if (oneTimeSound == false)
            {
                oneTimeSound = true;
                audioSource.Stop();
                audioSource.PlayOneShot(matchesFire);
            }
            timeBeforeDisable -= Time.deltaTime;

            // Guard : on ne lance qu'une seule fois
            if (timeBeforeDisable <= timeBeforeEndAnimation && !_autoReleased)
            {
                _autoReleased = true;   // pos� AVANT Launch() pour �viter tout re-d�clenchement
                charging = false;
                handMatches.SetActive(false);
                gotMatches = false;
                Launch(2f, burningFinger);
                //handAnimator.SetBool("Take", false);
                //Debug.Log("Bool Take = false (timeBeforeDisable <= timeBeforeEndAnimation && !_autoReleased)");
                StartCoroutine(TimeDisable());
            }
        }

        // Reset timeBeforeDisable s�par�ment, une fois qu'il est �puis�
        if (timeBeforeDisable < 0f)
        {
            timeBeforeDisable = 10.5f;
            cameraShake.StopShakeMatches();
            ConsumeAutoRelease();
        }
    }

    IEnumerator TimeDisable()
    {
        canThrow = false;
        yield return new WaitForSeconds(2f);
        //handAnimator.SetBool("Throw", false);
        handAnimator.Play("StartPos", 0, 0f);
        canThrow = true;
        //Debug.Log("Bool Throw = false (timeDisabled)");
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
                if (gotMatches == true) charging = true;
            }
        }
        else { Debug.Log("No matches left."); }
    }

    private void launchCanceled()
    {
        // Redevient simple, sans guard _autoReleased
        if (NumberOfMatches > 0 && matches != null ) //&& !leftHand.GetBoolTakeMatches()
        {
            if (keepInHand == false)
            {
                Launch(Force, throwMatches);
                charging = false;
            }
            else
            {
                if (gotMatches == true)
                {
                    handMatches.SetActive(false);
                    gotMatches = false;
                    //handAnimator.SetBool("Throw", true);
                    //handAnimator.SetBool("Take", false);
                    handAnimator.Play("ThrowMatches", 0, 0f);
                    //Debug.Log("Bool Throw = true, take = false (launch gotmaches = true)");
                    Launch(Force, throwMatches);
                    charging = false;
                }
                else if (gotMatches == false)
                {
                    StartCoroutine(Take());
                }
            }
        }
        else { Debug.Log("No matches left."); }
    }

    IEnumerator Take()
    {
        StartCoroutine(leftHand.waitAnimTakeMatches());
        yield return new WaitForSeconds(0.35f);
        //handAnimator.SetBool("Take", true);
        handAnimator.Play("TakeMatches", 0, 0f);
        yield return new WaitForSeconds(0.5f);
        if (!charging) handAnimator.Play("Cooldown", 0, 0f);
        Debug.Log("Bool take = true (Take courotines)");
        handMatches.SetActive(true);
        audioSource.Stop();
        audioSource.PlayOneShot(takeMatches);
        gotMatches = true;
        yield return null;
    }

    void Launch(float forceActual, AudioClip clip)
    {
        //Debug.Log("Launching matches.");
        audioSource.Stop();
        audioSource.PlayOneShot(clip);
        cameraShake.StopShakeMatches();
        NumberOfMatches--;
        GameObject matchesInstantiate = Instantiate(matches, transform.position, new Quaternion(0, 0.707106829f, 0, 0.707106829f));
        Rigidbody rb = matchesInstantiate.GetComponentInChildren<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * forceActual, ForceMode.Impulse);
        }
        Destroy(matchesInstantiate, timeBeforeDisable);
        timeBeforeDisable = 10.5f;

        //Force = 1; //  reset propre
    }
}
