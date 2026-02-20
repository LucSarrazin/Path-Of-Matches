using UnityEngine;

public class Inspectable : Interactable
{
    [SerializeField] private bool _freezeMovement = true;
    [SerializeField] private bool _freezeRotationLook = true;

    public override bool FreezeMovement => _freezeMovement;

    public override bool FreezeRotationLook => _freezeRotationLook;

    public override void Interact()
    {
        Debug.Log($"Inspecting : {gameObject.name}");
    }
}
