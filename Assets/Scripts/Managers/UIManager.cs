using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("General parameters : ")]
    [SerializeField] private PlayerReferences _playerReferences;

    private PlayerInteractions _playerInteractions;

    [Header("Display UI parameters : ")]

    [SerializeField] private TextMeshProUGUI _matches;
    [SerializeField] private TextMeshProUGUI _bpmCount;

    [Header("Pointer parameters : ")]
    [SerializeField] private Image _pointer;
    [SerializeField] private Color _defaultPointerColor = Color.black;
    [SerializeField] private Color _onFocusPointerColor = Color.red;

    private void OnStart()
    {
    }

    private void OnEnable()
    {
        _playerInteractions = _playerReferences.PlayerInteractions;
        _playerInteractions.OnFocusInteractable += ChangePointerColor;
    }

    private void OnDisable()
    {
        _playerInteractions.OnFocusInteractable -= ChangePointerColor;
    }

    private void ChangePointerColor(bool isFocusing)
    {
        if (isFocusing)
        {
            _pointer.color = _onFocusPointerColor;
        }
        else
        {
            _pointer.color = _defaultPointerColor;

        }
    }
}
