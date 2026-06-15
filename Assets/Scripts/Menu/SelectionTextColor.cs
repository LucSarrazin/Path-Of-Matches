using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private Color _normalColor = new Color (35, 45, 72, 1); // blue (default)
    [SerializeField] private Color _highlightedColor = new Color(191, 22, 34, 1); // red (default) 

    private void Awake()
    {
        if (_text == null)
        {
            _text = GetComponentInChildren<TMP_Text>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = _highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = _normalColor;
    }
}