using System.Collections;
using UnityEngine;

public class MatchesChest : Interactable
{
    [SerializeField] private Animator chestAnimator;
    [SerializeField] private AudioSource _chestSFXSource;
    private PlayerLaunchMatches launchMatches;
    [SerializeField] private GameObject matchesHUD;

    public override bool FreezeMovement => throw new System.NotImplementedException();

    public override bool FreezeRotationLook => throw new System.NotImplementedException();


    protected override void Awake()
    {
        base.Awake();

        if (_chestSFXSource == null) _chestSFXSource = GetComponentInChildren<AudioSource>();
    }
    protected override void Start()
    {
        base.Start();
        //launchMatches = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerLaunchMatches>();
        launchMatches = _playerReferences.PlayerLaunchMatches; //** moins co�teux qu'un FindGameObject
    }

    public override void Interact()
    {
            chestAnimator.SetBool("Open", true);
            _chestSFXSource.Play();
            chestAnimator.SetBool("Close", false);
            Debug.Log("Chest is Open");
            launchMatches.NumberOfMatches = 10;
            StartCoroutine(ShowUI());
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

    IEnumerator ShowUI()
    {
        matchesHUD.SetActive(true);
        yield return new WaitForSeconds(3f);
        matchesHUD.SetActive(false);
    }
}
