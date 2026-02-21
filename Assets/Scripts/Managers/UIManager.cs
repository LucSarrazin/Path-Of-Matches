using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("General parameters : ")]
    [SerializeField] private PlayerReferences _playerReferences;

    private PlayerInteractions _playerInteractions;
    private PlayerLaunchMatches _playerLaunchMatches;

    [Header("Display UI parameters : ")]

    [SerializeField] private TextMeshProUGUI _matches;
    [SerializeField] private TextMeshProUGUI _bpmCount;
    [SerializeField] private Slider _forceSlider;

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
        _playerLaunchMatches = _playerReferences.PlayerLaunchMatches;

        _playerInteractions.OnFocusInteractable += ChangePointerColor;

        _playerLaunchMatches.OnChangeNumberOfMatches += UpdateNumberOfMatchesIndicator; 
        _playerLaunchMatches.OnForceChange += UpdateForceIndicator;


        /* - First Update - */
        InitalConfigForceIndicator();
        UpdateNumberOfMatchesIndicator(_playerLaunchMatches.NumberOfMatches); 
        UpdateForceIndicator(_playerLaunchMatches.Force); 

    }

    private void OnDisable()
    {
        _playerInteractions.OnFocusInteractable -= ChangePointerColor;
        _playerLaunchMatches.OnChangeNumberOfMatches -= UpdateNumberOfMatchesIndicator; 
        _playerLaunchMatches.OnForceChange -= UpdateForceIndicator; 
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

    private void InitalConfigForceIndicator()
    {
        _forceSlider.minValue = 1f; 
        _forceSlider.maxValue = 10f;
    }

    private void UpdateNumberOfMatchesIndicator(int numberOfMatches) => _matches.text = numberOfMatches.ToString();
    private void UpdateForceIndicator(float force) => _forceSlider.value = force;
    private void UpdateInsanityIndicator(float insanity) => _bpmCount.text = insanity.ToString();
}
