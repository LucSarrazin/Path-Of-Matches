using UnityEngine; 

public class CubeInteractable : Interactable
{
    public override bool FreezeMovement => throw new System.NotImplementedException();

    public override bool FreezeRotationLook => throw new System.NotImplementedException();

    public override void Interact()
    {
        base.Interact();
    }
}
