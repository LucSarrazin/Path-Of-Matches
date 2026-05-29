using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private PlayerReferences _playerReferences;
    private Camera _viewCamera;
    private LayerMask _interactibleLayerMask;
    private float _checkDistance;

    private Interactable _currentInteractable;
    public Interactable CurrentInteractable => _currentInteractable;

    private void Awake()
    {
        if (_playerReferences == null)
        {
            _playerReferences = GetComponentInParent<PlayerReferences>();
            Debug.Log($" - GO : {this} -> script 'PlayerControls' charged by GetComponent.");
        }

        _viewCamera = _playerReferences.PlayerViewCamera;
        _checkDistance = _playerReferences.CheckDistance;
        _interactibleLayerMask = _playerReferences.InteractibleLayer;
    }

    /* --- Interact methods --- */

    private bool CanInteract(out RaycastHit hit)
    {
        Ray ray = new Ray(_viewCamera.transform.position, _viewCamera.transform.forward);
        return Physics.Raycast(ray, out hit, _checkDistance, _interactibleLayerMask);
    }

    private void GetInteractable() /* And active focus method */
    {
        if (CanInteract(out RaycastHit hit))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
                //Debug.Log($"Hit {interactable.gameObject.name}");

            if (interactable != _currentInteractable)
            {
                _currentInteractable?.LoseFocus();

                if (_currentInteractable != null) OnFocusInteractable?.Invoke(false);

                _currentInteractable = interactable;
                _currentInteractable?.OnFocus();
                OnFocusInteractable?.Invoke(true);
                Debug.Log($"Focus {interactable.gameObject.name}");
            }
        }
        else
        {
            _currentInteractable?.LoseFocus();
            if (_currentInteractable != null) OnFocusInteractable?.Invoke(false);
            _currentInteractable = null;
        }
    }

    public void TryInteract()
    {
        _currentInteractable?.Interact();
    }

    /* --- Update check for UX highlight focus --- */

    private void Update()
    {
        GetInteractable();
    }

    /* --- Events --- */

    public Action<bool> OnFocusInteractable;

    /* --- Editor Scripting --- */
    private void OnDrawGizmos()
    {
        _viewCamera = _playerReferences.PlayerViewCamera;
        _checkDistance = _playerReferences.CheckDistance;

        Gizmos.color = Color.red;
        Ray ray = new Ray(_viewCamera.transform.position, _viewCamera.transform.forward);

        Gizmos.DrawRay(ray.origin, ray.direction * _checkDistance);
    }


}
