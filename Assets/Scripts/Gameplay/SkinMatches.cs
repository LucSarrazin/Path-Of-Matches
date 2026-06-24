using UnityEngine;

public class SkinMatches : Interactable
{
    [SerializeField] private GameObject skin;
    [SerializeField] private bool oneTime;

    [SerializeField] private AudioSource _matchesBoxSource; 

    public override bool FreezeMovement => throw new System.NotImplementedException();

    public override bool FreezeRotationLook => throw new System.NotImplementedException();

    //protected override void Start()
    //{
    //    base.Start();
    //    // if (skin.GetComponent<Matches>().possessed == "True")
    //    // {
    //    //     Destroy(gameObject);
    //    // }
    //}

    protected override void Awake()
    {
        base.Awake();

        if (_matchesBoxSource == null) _matchesBoxSource = GetComponentInChildren<AudioSource>();
    }

    public override void Interact()
    {
        if (oneTime) return;
        
        _matchesBoxSource.Play();

        oneTime = true;
        // -- Adds the skin to the player's skin list when they interact with -- //
        _playerReferences.PlayerSwitchMatches.AddMatchesSkin(skin);
        _playerReferences.PlayerLaunchMatches.NumberOfMatches = 10;


        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        gameObject.layer = 0;
        this.enabled = false;
    }
}
