using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    private Vector2 _moveInputs;
    public Vector2 MoveInputs => _moveInputs;


    public void MoveInputsCallback(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _moveInputs = context.ReadValue<Vector2>();
            Debug.Log("Arrow activated");
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _moveInputs = Vector2.zero;
            Debug.Log("Arrow canceled");
        }
    }


}
