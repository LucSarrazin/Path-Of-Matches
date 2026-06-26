using System.Collections;
using UnityEngine;

public class Watch : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool switched = false;
    private bool isTakingMatches = false;

    public bool IsTakingMatches => isTakingMatches;

    // Ajout un peu beaucoup schlag pour tester, a revoir avec Luc pour modif de son script 
    //Juste pour patch rapide dans PlayerInteractState( -> Ranger montre en mode inspection) 
    public void SetBoolSwitch(bool value) { switched = value; }
    public bool GetBoolTakeMatches() { return isTakingMatches; }


    public void openWatch()
    {
        if (!isTakingMatches)
        {
            if (!switched)
            {
                switched = true;
                Debug.Log("Close Watch");
                animator.SetBool("Close", true);
            }
            else
            {
                switched = false;
                Debug.Log("Open Watch");
                animator.SetBool("Close", false);
            }
        }
    }

    public IEnumerator waitAnimTakeMatches()
    {
        if (!switched)
        {
            isTakingMatches = true;
            Debug.Log("Close Watch");
            animator.SetBool("Close", true);
            yield return new WaitForSeconds(0.9f);
            Debug.Log("Open Watch");
            animator.SetBool("Close", false);
            isTakingMatches = false;
        }
        else
        {
            isTakingMatches = true;
            yield return new WaitForSeconds(0.9f);
            isTakingMatches = false;
        }
    }
}
