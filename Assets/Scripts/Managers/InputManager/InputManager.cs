using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionAsset actions;
    public InputAction move;
    public InputAction interact;
    public InputAction throwAction;
    public InputAction runAction;
    public InputAction switchMatchesAction;

    void Awake()
    {
        move = actions.FindAction("Move");
        interact = actions.FindAction("Interact");
        throwAction = actions.FindAction("Throw");
        runAction = actions.FindAction("Run");
        switchMatchesAction = actions.FindAction("SwitchMatch");

        Load();
    }

    void OnEnable()
    {
        move.Enable();
        interact.Enable();
        throwAction.Enable();
    }

    void OnDisable()
    {
        move.Disable();
        interact.Disable();
        throwAction.Disable();
    }

    public Vector2 GetMove()
    {
        return move.ReadValue<Vector2>();
    }

    public bool InteractPressed()
    {
        return interact.WasPressedThisFrame();
    }

    public bool ThrowPressed()
    {
        return throwAction.WasPressedThisFrame();
    }

    public void Save()
    {
        PlayerPrefs.SetString(
            "rebinds",
            actions.SaveBindingOverridesAsJson()
        );
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("rebinds"))
        {
            actions.LoadBindingOverridesFromJson(
                PlayerPrefs.GetString("rebinds")
            );
        }
    }
}
