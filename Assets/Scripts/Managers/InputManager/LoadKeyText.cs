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

        UpdateKeyText();
    }

    public void UpdateKeyText()
    {
        text.text = actionReference.action.GetBindingDisplayString(bindingIndex);
    }
}