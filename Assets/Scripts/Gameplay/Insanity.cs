using UnityEngine;
using System.Collections;

public class Insanity : MonoBehaviour
{
    [SerializeField]
    private int insanityLvl;
    private bool wait;

    public int InsanityLvl
    {
        get { return insanityLvl; }
        private set { insanityLvl = value; }
    }

    void Start()
    {
        ResetInsanity();
    }

    void Update()
    {
        if (wait == false && (DarkZone.IsInDarkZone || insanityLvl > 0))
        {
            wait = true;
            StartCoroutine(UpdateInsanity());
        }

        //Game ends if the madness level is above 4
        if (insanityLvl == 5)
        {
            Debug.Log("Death");
        }
    }

    public void IncreaseInsanity()
    {
        //Prevents madness from exceeding the maximum level
        if (InsanityLvl == 5)
            return;
        else
            InsanityLvl++;
    }

    public void DecreaseInsanity()
    {
        //Prevents madness from falling below the lowest level
        if (InsanityLvl == 0)
            return;
        else
            InsanityLvl--;
    }

    public void ResetInsanity()
    {
        InsanityLvl = 0;
    }

    public void ChooseInsanity(int lvl)
    {
        //Prevents the selection of an invalid madness level
        if (lvl < 0)
            lvl = 0;
        else if (lvl > 5)
            lvl = 5;
        InsanityLvl = lvl;
    }

    IEnumerator UpdateInsanity()
    {
        //allows to take a break between each insanity level update
        yield return new WaitForSeconds(5f);

        if (DarkZone.IsInDarkZone && SafeZone.IsInSafeZone == false)
            IncreaseInsanity();
        else
            DecreaseInsanity();

        wait = false;
    }
}