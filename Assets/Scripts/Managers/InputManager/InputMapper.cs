using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class InputMapper : MonoBehaviour
{
    GameObject clickedButton;
    bool isEditing = false;
    InputBinding newBinding; 
    public InputActionReference actionReference;

    public void OnButtonClick(int bindingIndex)
    {
        // -- Activating key editing mode when a key editing button is pressed -- //

        if (isEditing) return; // Check if the player is already editing another key //

        clickedButton = EventSystem.current.currentSelectedGameObject;

        clickedButton.transform.GetChild(0)
            .GetComponent<TMP_Text>().text = "?";

        StartRebind(bindingIndex);
    }

    void StartRebind(int bindingIndex)
    {
        // -- key editing mode -- //

        isEditing = true;

        actionReference.action.Disable(); // disables all player controls //

        // -- When a new key is pressed -- //

        actionReference.action.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(operation =>
            {
                actionReference.action.Enable(); // Enables all player controls //

                operation.Dispose();

                // Change the display of the old key in settings to the new one //
                string newKey = actionReference.action.GetBindingDisplayString(bindingIndex);

                clickedButton.transform.GetChild(0)
                    .GetComponent<TMP_Text>().text = newKey;
                // ------------------------------------------------------------ //

                Save(); 

                isEditing = false;
            })
            .Start();
    }

    void Save()
    {
        // -- Save player controls -- //

        PlayerPrefs.SetString(
            "rebinds",
            actionReference.action.actionMap.asset.SaveBindingOverridesAsJson()
        );
    }
}
