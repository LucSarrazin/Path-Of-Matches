using System;
using UnityEngine; 

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [Header("[INTERACTABLE] GENERAL SETTINGS ")]
    [SerializeField] protected PlayerReferences _playerReferences;
    [Tooltip("WARNING : For read-only or test to adjust, please change value in script to keep logic")]
    [SerializeField] private float _focusSpriteDistance = 0.7f;

    /* --- Display focus on raycast --- */

    private Transform _interactableTransform;
    private GameObject _focusSprite;

    protected Renderer _renderer;
    protected Color _baseColor;

    public abstract bool FreezeMovement { get; }
    public abstract bool FreezeRotationLook { get; }

    protected virtual void Awake()
    {
        _interactableTransform = this.transform;
        _focusSprite = _playerReferences.InteractibleFocusSprite;

        _renderer = GetComponent<Renderer>();

        _baseColor = _renderer.material.color;
    }

    public virtual void OnFocus()
    {
        if (_renderer == null)
        {
            Debug.Log("Can't access to renderer ");
            return; 
        }

        if (_focusSprite == null)
        {
            Debug.Log("Can't access to focus Sprite ");
            return;
        }

        _focusSprite.SetActive(true);
        _focusSprite.transform.position = _interactableTransform.position + Vector3.up * _focusSpriteDistance;
        _focusSprite.transform.LookAt(_playerReferences.PlayerViewCamera.transform);
        _renderer.material.color = Color.green;

    }

    public virtual void LoseFocus()
    {
        _focusSprite.SetActive(false);

        if (_renderer != null) _renderer.material.color = _baseColor;
    }

    /* --- Interactions --- */
    public virtual void Interact()
    {
        Debug.Log($"Interacting with : {this.gameObject.name}");
    }

}
