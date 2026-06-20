using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Insanity : MonoBehaviour
{
    [Header("[SETTINGS]")]
    [SerializeField] private int insanityLvl;
    [SerializeField] public int addInsanity = 3;
    [SerializeField] public int loseInsanity = 3;
    [SerializeField] public int loseInsanityMatches = 1;

    [Header("[ANIMATION]")]
    [SerializeField] private Animator animator;

    private bool wait;
    public static bool isDead;

    public int InsanityLvl
    {
        get => insanityLvl;
        set
        {
            if (insanityLvl == value) return;
            insanityLvl = value;
            OnInsanityChange?.Invoke(insanityLvl);
        }
    }

    /* --- Events --- */
    public Action<int> OnInsanityChange;

    private void Awake()
    {
        ResetInsanity();
    }

    void Update()
    {
        if (wait == false && (DarkZone.IsInDarkZone || insanityLvl > 70))
        {
            wait = true;
            StartCoroutine(UpdateInsanity());
        }

        //Game ends if the madness level is above 4
        if (insanityLvl == 150 && !isDead)
        {
            isDead = true;
            GameEvents.OnPlayerDeath?.Invoke(); //Add Event on Player's Death
            Debug.Log("Death");
        }

        if (insanityLvl >= 70 && insanityLvl < 90)
        {
            animator.SetBool("Hard", false);
            animator.SetBool("Mid", false);
            animator.SetBool("Low", true);
        }

        if (insanityLvl >= 90 && insanityLvl < 125)
        {
            animator.SetBool("Hard", false);
            animator.SetBool("Mid", true);
            animator.SetBool("Low", false);
        }

        if (insanityLvl >= 125 && insanityLvl < 150)
        {
            animator.SetBool("Hard", true);
            animator.SetBool("Mid", false);
            animator.SetBool("Low", false);
        }
    }

    public void IncreaseInsanity(int n)
    {
        //Prevents madness from exceeding the maximum level
        InsanityLvl += n;
        if (InsanityLvl > 150)
            InsanityLvl = 150;
    }

    public void DecreaseInsanity(int n)
    {
        //Prevents madness from falling below the lowest level
        InsanityLvl -= n;
        if (InsanityLvl < 70)
            InsanityLvl = 70;
    }

    public void ResetInsanity()
    {
        InsanityLvl = 70;
    }

    public void ChooseInsanity(int lvl)
    {
        //Prevents the selection of an invalid madness level
        if (lvl < 70)
            lvl = 70;
        else if (lvl > 150)
            lvl = 150;
        InsanityLvl = lvl;
    }

    IEnumerator UpdateInsanity()
    {
        //allows to take a break between each insanity level update
        yield return new WaitForSeconds(2.5f);

        if (DarkZone.IsInDarkZone && SafeZone.IsInSafeZone == false && SafeZone.IsInMatcheZone == false)
            IncreaseInsanity(addInsanity);
        else
        {
            if (SafeZone.IsInMatcheZone && SafeZone.IsInSafeZone == false)
                DecreaseInsanity(loseInsanityMatches);
            else
                DecreaseInsanity(loseInsanity);
        }

        wait = false;
    }
}