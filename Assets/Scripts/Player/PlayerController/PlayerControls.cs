using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    private Vector2 _moveInputs;
    public Vector2 MoveInputs => _moveInputs;

    private Vector2 _lookInputs;
    public Vector2 LookInputs => _lookInputs;

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

    }



}
