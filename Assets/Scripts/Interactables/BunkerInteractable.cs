using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BunkerInteractable : Interactable
{
    [SerializeField] private string mapName;
    [SerializeField] private UnityEvent onInteract;
    public override bool FreezeMovement => throw new System.NotImplementedException();
    public override bool FreezeRotationLook => throw new System.NotImplementedException();


    public override void Interact()
    {
        Debug.Log("Interacting with Bunker Interactable");
        onInteract?.Invoke();
    }
}
