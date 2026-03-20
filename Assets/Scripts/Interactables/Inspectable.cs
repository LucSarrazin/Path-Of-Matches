using UnityEngine;

public class Inspectable : Interactable
{

    [Header("SETTINGS :")]
    [SerializeField] private bool _freezeMovement = true;
    [SerializeField] private bool _freezeRotationLook = true;

    [Header("References")]
    [SerializeField] private InspectableObjectData _data;

    public override bool FreezeMovement => _freezeMovement;
    public override bool FreezeRotationLook => _freezeRotationLook;

    public override void Interact()
    {
        UIManager.Instance.ToggleInspectionPanel(_data);

        Debug.Log($"Inspecting : {gameObject.name}");
    }
}
