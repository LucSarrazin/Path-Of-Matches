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



}
