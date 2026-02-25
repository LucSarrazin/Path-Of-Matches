using System.Collections;
using UnityEngine;

public class MatchesChest : Interactable
{
    [SerializeField] private Animator chestAnimator;
    private PlayerLaunchMatches launchMatches;

    public override bool FreezeMovement => throw new System.NotImplementedException();

    public override bool FreezeRotationLook => throw new System.NotImplementedException();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        launchMatches = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerLaunchMatches>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
            chestAnimator.SetBool("Open", true);
            chestAnimator.SetBool("Close", false);
            Debug.Log("Chest is Open");
            launchMatches.NumberOfMatches = 5;
            StartCoroutine(OpenChest());
    }

    IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(0.5f);
        chestAnimator.SetBool("Open", false);
        chestAnimator.SetBool("Close", true);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Chest is closed");
    }
}
