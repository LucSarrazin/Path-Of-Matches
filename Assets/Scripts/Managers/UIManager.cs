using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } /* SINGLETON : to call Manager UI Panels in scripts as "Inspectable" (Automatic reference in Inheritance scripts) */

    [Header("[GENERAL] references : ")]
    [SerializeField] private PlayerReferences _playerReferences;

    private PlayerInteractions _playerInteractions;
    private PlayerLaunchMatches _playerLaunchMatches;
    private Insanity _playerInsanity;

    [Header("[OVERVIEW] SETTINGS : ")]

    [Header("Panel texts settings :")]
    [SerializeField] private TextMeshProUGUI _matches;
    [SerializeField] private TextMeshProUGUI _bpmCount;
    [SerializeField] private Slider _forceSlider;

    [Header("Pointer settings : ")]
    [SerializeField] private Image _pointer;
    [SerializeField] private Color _defaultPointerColor = Color.black;
    [SerializeField] private Color _onFocusPointerColor = Color.red;

    [Header("[INSPECTION] SETTINGS : ")]
    [SerializeField] private GameObject _inspectionPanel;
    [SerializeField] private TextMeshProUGUI _textNameObject;
    [SerializeField] private TextMeshProUGUI _textDescription;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //DontDestroyOnLoad(gameObject); /*If we want to keep it between scenes*/
    }

    private void OnEnable()
    {
        _playerInteractions = _playerReferences.PlayerInteractions;
        _playerLaunchMatches = _playerReferences.PlayerLaunchMatches;
        _playerInsanity = _playerReferences.PlayerInsanity;

        _playerInteractions.OnFocusInteractable += ChangePointerColor;
        _playerLaunchMatches.OnChangeNumberOfMatches += UpdateNumberOfMatchesIndicator;
        _playerLaunchMatches.OnForceChange += UpdateForceIndicator;
        _playerInsanity.OnInsanityChange += UpdateInsanityIndicator;


        /* - First Update - */
        InitalConfigForceIndicator();
        UpdateNumberOfMatchesIndicator(_playerLaunchMatches.NumberOfMatches);
        UpdateForceIndicator(_playerLaunchMatches.Force);
        UpdateInsanityIndicator(_playerInsanity.InsanityLvl);

    }

    private void OnDisable()
    {
        _playerInteractions.OnFocusInteractable -= ChangePointerColor;
        _playerLaunchMatches.OnChangeNumberOfMatches -= UpdateNumberOfMatchesIndicator;
        _playerLaunchMatches.OnForceChange -= UpdateForceIndicator;
        _playerInsanity.OnInsanityChange -= UpdateInsanityIndicator;

    }

    // * --- Methods for Overview panel --- * //

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
    private void UpdateInsanityIndicator(int insanity) => _bpmCount.text = insanity.ToString();

    /* - Public method to change sensitivity of the pointer -> maybe to move on other script "UI" made by luc dedicated to Pause Menu ? | Or keep it in this general UI Manager ? | Or made a "Game Settings" Script ? - */
    public void OnPointerSensitivityChanged(float value)
    {
        _playerReferences.PointerSensitivity = value;
    }

    // * --- Methods for Inspection panel --- * //

    public void ToggleInspectionPanel(InspectableObjectData data)
    {
        _inspectionPanel.SetActive(!_inspectionPanel.activeSelf);
        _textNameObject.text = data.Name;
        _textDescription.text = data.Description;

    }

    // * --- General Methods --- * //

}
