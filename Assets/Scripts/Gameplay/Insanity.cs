using UnityEngine;
using System.Collections;
using System;

public class Insanity : MonoBehaviour
{
    [SerializeField]
    private int insanityLvl;
    private bool wait;

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

    void Start()
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
        if (insanityLvl == 150)
        {
            Debug.Log("Death");
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
        yield return new WaitForSeconds(1f);

        if (DarkZone.IsInDarkZone && SafeZone.IsInSafeZone == false)
            IncreaseInsanity(3);
        else
            DecreaseInsanity(3);

        wait = false;
    }
}