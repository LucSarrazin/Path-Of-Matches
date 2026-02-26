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
        // -- Variable initialization -- //

        move = actions.FindAction("Move");
        interact = actions.FindAction("Interact");
        throwAction = actions.FindAction("Throw");
        runAction = actions.FindAction("Run");
        switchMatchesAction = actions.FindAction("SwitchMatch");

        Load();
    }

    void OnEnable()
    {
        // -- Activates all player controls -- //

        move.Enable();
        interact.Enable();
        throwAction.Enable();
        runAction.Enable();
        switchMatchesAction.Enable();
    }

    void OnDisable()
    {
        // -- Disables all player controls -- //

        move.Disable();
        interact.Disable();
        throwAction.Disable();
        runAction.Disable();
        switchMatchesAction.Disable();
    }
    public void Save()
    {
        // -- Save player controls -- //

        PlayerPrefs.SetString(
            "rebinds",
            actions.SaveBindingOverridesAsJson()
        );
    }

    public void Load()
    {
        // -- reclaims player controls -- //

        if (PlayerPrefs.HasKey("rebinds"))
        {
            actions.LoadBindingOverridesFromJson(
                PlayerPrefs.GetString("rebinds")
            );
        }
    }
}
