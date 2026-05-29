using UnityEngine;

public class MatchesBox : Interactable
{
    [SerializeField] private GameObject skin;
    [SerializeField] private bool oneTime;

    public override bool FreezeMovement => throw new System.NotImplementedException();

    public override bool FreezeRotationLook => throw new System.NotImplementedException();

    public override void Interact()
    {
        if (oneTime) return;

        oneTime = true;
        // -- Adds the skin to the player's skin list when they interact with -- //
        _playerReferences.PlayerSwitchMatches.AddMatchesSkin(skin);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }
}