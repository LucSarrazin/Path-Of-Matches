using Unity.VisualScripting;
using UnityEngine;

public class SkinMatches : Interactable
{
    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private GameObject skin;

    public override bool FreezeMovement => throw new System.NotImplementedException();

    public override bool FreezeRotationLook => throw new System.NotImplementedException();

    private void Start()
    {
        if (skin.GetComponent<Matches>().possessed == "True")
        {
            Destroy(gameObject);
        }
    }

    public override void Interact()
    {
        // -- Adds the skin to the player's skin list when they interact with -- //
        _playerReferences.PlayerSwitchMatches.AddMatchesSkin(skin);

        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject);
    }
}
