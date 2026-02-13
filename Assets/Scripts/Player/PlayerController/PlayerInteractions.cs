using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private PlayerReferences _playerReferences;
    private Camera _viewCamera;
    private LayerMask _interactibleLayerMask;
    private float _checkDistance;

    private IInteractable _currentInteractable;

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
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != _currentInteractable)
            {
                _currentInteractable?.LoseFocus();
                _currentInteractable = interactable;
                _currentInteractable?.OnFocus();
            }
        }
        else
        {
            _currentInteractable?.LoseFocus();
            _currentInteractable = null;
        }
    }

    public void TryInteract()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }

    /* --- Update check for UX highlight focus --- */

    private void Update()
    {
        GetInteractable();
    }

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
