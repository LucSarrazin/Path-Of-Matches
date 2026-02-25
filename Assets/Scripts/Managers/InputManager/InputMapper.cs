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
        if (isEditing) return;

        clickedButton = EventSystem.current.currentSelectedGameObject;

        clickedButton.transform.GetChild(0)
            .GetComponent<TMP_Text>().text = "?";

        StartRebind(bindingIndex);
    }

    void StartRebind(int bindingIndex)
    {
        isEditing = true;

        actionReference.action.Disable();

        actionReference.action.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(operation =>
            {
                actionReference.action.Enable();

                operation.Dispose();

                string newKey = actionReference.action.GetBindingDisplayString(bindingIndex);

                clickedButton.transform.GetChild(0)
                    .GetComponent<TMP_Text>().text = newKey;

                Save();

                isEditing = false;
            })
            .Start();
    }

    void Save()
    {
        PlayerPrefs.SetString(
            "rebinds",
            actionReference.action.actionMap.asset.SaveBindingOverridesAsJson()
        );
    }
}
