using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [Header("SETTINGS : ")]
    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private PlayerMovements _playerMovements;

    private Vector2 _moveInputs;
    public Vector2 MoveInputs => _moveInputs;

    private Vector2 _lookInputs;
    public Vector2 LookInputs => _lookInputs;

    private bool _wantToRun;
    public bool WantToRun => _wantToRun;

    private bool _wantToInteract;
    public bool WantToInteract => _wantToInteract;

    private bool _wantToThrow;
    public bool WantToThrow => _wantToThrow;

    public float DragValue { get; private set; }

    private bool _wantToSwitchMatch;
    public bool WantToSwitchMatch => _wantToSwitchMatch;

    public bool IsInspecting { get; set; }

    private void Awake()
    {
        if (_playerMovements == null) { _playerMovements = GetComponent<PlayerMovements>(); }
    }

    public void MoveInputsCallback(InputAction.CallbackContext context)
    {
        if (IsInspecting) return;
        if (context.phase == InputActionPhase.Performed)
        {
            _moveInputs = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _moveInputs = Vector2.zero;
        }
    }

    public void LookInputsCallback(InputAction.CallbackContext context)
    {
        //if (IsInspecting) return;
        _lookInputs = context.ReadValue<Vector2>();

        //Debug.Log($"Mouse delta {_lookInputs} | Phase : {context.phase} ");

        _playerMovements.SetLookInputs(_lookInputs);
    }

    public void RunInputCallback(InputAction.CallbackContext context)
    {
        if (IsInspecting) return;
        if (context.phase == InputActionPhase.Performed)
        {
            _wantToRun = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _wantToRun = false;
        }
    }

    public void InteractInputCallback(InputAction.CallbackContext context)
    {
        
        if (context.started) /* "Started" for only pressed this frame */
        {
            _wantToInteract = true;
        }

        //if (context.phase == InputActionPhase.Performed)
        //{
        //    _wantToInteract = true;
        //}
        //else if (context.phase == InputActionPhase.Canceled)
        //{
        //    _wantToInteract = false;
        //}
    }

    public void ThrowInputCallback(InputAction.CallbackContext context)
    {
        if (IsInspecting) return;

        if (context.phase == InputActionPhase.Performed)
        {
            _wantToThrow = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _wantToThrow = false;
        }
    }

    public bool IsDraggingInspectable { get; private set; }

    public void SwitchMatchInputCallback(InputAction.CallbackContext context)
    {
        bool isInspecting = _playerReferences.PlayerControllerSM.CurrentActionState
                            == _playerReferences.PlayerControllerSM.ActionStates.Interact;

        if (isInspecting)
        {
            if (context.phase == InputActionPhase.Performed)
                IsDraggingInspectable = true;
            else if (context.phase == InputActionPhase.Canceled)
                IsDraggingInspectable = false;

            // WantToSwitchMatch reste false : l'autre action est neutralisée
            _wantToSwitchMatch = false;
        }
        else
        {
            IsDraggingInspectable = false;
            if (context.phase == InputActionPhase.Performed)
                _wantToSwitchMatch = true;
            else if (context.phase == InputActionPhase.Canceled)
                _wantToSwitchMatch = false;
        }
    }

    private void LateUpdate()
    {
        _wantToInteract = false; /* Reset to keep one frame only */
    }

}
