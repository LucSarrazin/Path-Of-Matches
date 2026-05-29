using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [Header("[INTERACTABLE] GENERAL SETTINGS ")]
    [SerializeField] protected PlayerReferences _playerReferences;
    //[Tooltip("WARNING : For read-only or test to adjust, please change value in script to keep logic")]//
    [Tooltip("WARNING : To calibrate distance between transform to sprite")]
    [SerializeField] private float _focusSpriteDistance = 0.7f;

    /* --- Display focus on raycast --- */

    [Header("[INTERACTABLE] FOCUS SETTINGS ")]
    private Outline _outline;

    private Color _outlineColor;
    private float _outlineWidth;

    private Transform _interactableTransform;
    private Vector3 _interactableInitialPosition; 
    private GameObject _focusSprite;

    public abstract bool FreezeMovement { get; }
    public abstract bool FreezeRotationLook { get; }

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {

        _interactableTransform = this.transform;
        _interactableInitialPosition = _interactableTransform.position; 
        _focusSprite = _playerReferences.InteractibleFocusSprite;
        _focusSprite.SetActive(false);
        
        _outlineColor = UIManager.Instance.OutlineColor;
        _outlineWidth = UIManager.Instance.OutlineWidth;

        if (!TryGetComponent(out _outline))
        {
            _outline = gameObject.AddComponent<Outline>();
            Debug.Log($"Outline component added to {gameObject.name}");
        }

        _outline.OutlineMode = Outline.Mode.OutlineVisible;
        _outline.OutlineColor = _outlineColor;
        _outline.OutlineWidth = _outlineWidth;
        _outline.enabled = false;

        Debug.Log($"Try add outline initialization to : {gameObject.name}");

    }


    public virtual void OnFocus()
    {

        if (_outline == null)
        {
            Debug.Log($"Can't access to outline Component : {gameObject.name}");
            return;
        }

        if (_focusSprite == null)
        {
            Debug.Log($"Can't access to focus Sprite : {gameObject.name} ");
            return;
        }

        _focusSprite.SetActive(true);

        _focusSprite.transform.position = _interactableInitialPosition + Vector3.up * _focusSpriteDistance;
        _focusSprite.transform.LookAt(_playerReferences.PlayerViewCamera.transform);

        _outline.enabled = true;

    }

    public virtual void LoseFocus()
    {
        if (_focusSprite != null)
            _focusSprite.SetActive(false);

        if (_outline != null)
            _outline.enabled = false;
    }

    /* --- Interactions --- */
    public virtual void Interact()
    {
        Debug.Log($"Interacting with : {this.gameObject.name}");
    }

}
