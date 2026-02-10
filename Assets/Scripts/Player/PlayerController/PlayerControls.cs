using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private PlayerMovements _playerMovements;
    private Vector2 _moveInputs;
    public Vector2 MoveInputs => _moveInputs;

    private Vector2 _lookInputs;
    public Vector2 LookInputs => _lookInputs;

    private bool _wantToRun;
    public bool WantToRun => _wantToRun;

    private bool _wantToInteract;
    public bool WantToInteract => _wantToInteract;

    private void Awake()
    {
        if (_playerMovements == null) { _playerMovements = GetComponent<PlayerMovements>(); }
    }

    public void MoveInputsCallback(InputAction.CallbackContext context)
    {
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
        _lookInputs = context.ReadValue<Vector2>();

        //Debug.Log($"Mouse delta {_lookInputs} | Phase : {context.phase} ");

        _playerMovements.SetLookInputs(_lookInputs);
    }

    public void RunInputCallback(InputAction.CallbackContext context)
    {
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
        if (context.phase == InputActionPhase.Performed)
        {
            _wantToInteract = true;
            Debug.Log("Player want to interact.");
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _wantToInteract = false;
        }
    }


}
