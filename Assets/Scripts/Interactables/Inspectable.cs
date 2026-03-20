using UnityEngine;

public class Inspectable : Interactable
{

    [Header("SETTINGS :")]
    [SerializeField] private bool _freezeMovement = true;
    [SerializeField] private bool _freezeRotationLook = true;

    public override bool FreezeMovement => _freezeMovement;
    public override bool FreezeRotationLook => _freezeRotationLook;

    private bool _isInspecting = false; 

    public override void Interact()
    {
        UIManager.Instance.ToggleInspectionPanel();
        Debug.Log($"Inspecting : {gameObject.name}");
    }
}
