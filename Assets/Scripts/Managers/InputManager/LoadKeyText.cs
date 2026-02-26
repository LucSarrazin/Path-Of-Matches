using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class LoadKeyText : MonoBehaviour
{
    public InputActionReference actionReference;
    public int bindingIndex;

    TMP_Text text;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<TMP_Text>();

        // Loads the display of saved keys when the game is launched in the menus //
        UpdateKeyText();
    }

    public void UpdateKeyText()
    {
        // -- updates the display of keys in the menu -- //

        text.text = actionReference.action.GetBindingDisplayString(bindingIndex);
    }
}