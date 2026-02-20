using System;
using UnityEngine; 

public abstract class Interactable : MonoBehaviour, IInteractable
{
    /* --- Display focus on raycast --- */

    protected Renderer _renderer;
    protected Color _baseColor;

    public abstract bool FreezeMovement { get; }
    public abstract bool FreezeRotationLook { get; }

    protected virtual void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _baseColor = _renderer.material.color;

    }

    public virtual void OnFocus()
    {
        _renderer.material.color = Color.green;
        
    }

    public virtual void LoseFocus()
    {
        _renderer.material.color = _baseColor;
    }

    /* --- Interactions --- */
    public virtual void Interact()
    {
        Debug.Log($"Player is interacting with {this.gameObject.name}");
    }

}
