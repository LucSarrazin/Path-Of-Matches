using UnityEngine;
using System.Collections;

public class Insanity : MonoBehaviour
{
    [SerializeField]
    private int insanityLvl = 0;
    private DarkZone detector;
    private bool wait;

    void Start()
    {
        ResetInsanity();
        detector = FindAnyObjectByType<DarkZone>();
    }

    void Update()
    {
        if (wait == false && (detector.IsInDarkZone || insanityLvl > 0))
        {
            wait = true;
            StartCoroutine(UpdateInsanity());
        }

        //Game ends if the madness level is above 4
        if (insanityLvl == 4)
        {
            Debug.Log("Death");
        }
    }

    public void IncreaseInsanity()
    {
        //Prevents madness from exceeding the maximum level
        if (insanityLvl == 4)
            return;
        else
            insanityLvl++;
    }

    public void DecreaseInsanity()
    {
        //Prevents madness from falling below the lowest level
        if (insanityLvl == 0)
            return;
        else
            insanityLvl--;
    }

    public void ResetInsanity()
    {
        insanityLvl = 0;
    }

    public void ChooseInsanity(int lvl)
    {
        //Prevents the selection of an invalid madness level
        if (lvl < 0)
            lvl = 0;
        else if (lvl > 4)
            lvl = 4;
        insanityLvl = lvl;
    }

    IEnumerator UpdateInsanity()
    {
        //allows to take a break between each insanity level update
        yield return new WaitForSeconds(5f);

        if (detector.IsInDarkZone)
            IncreaseInsanity();
        else
            DecreaseInsanity();

        wait = false;
    }
}