using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] private Image _forceSliderTest;

    [Header("Pointer settings : ")]
    [SerializeField] private Image _pointer;
    [SerializeField] private Color _defaultPointerColor = Color.black;
    [SerializeField] private Color _onFocusPointerColor = Color.red;

    [Header("[INSPECTION] SETTINGS : ")]
    [SerializeField] private GameObject _inspectionPanel;
    [SerializeField] private TextMeshProUGUI _textNameObject;
    [SerializeField] private TextMeshProUGUI _textDescription;
    [SerializeField] private Color _outlineColor = new Color(1f, 1f, 1f, 0.8f);
    [SerializeField] private float _outlineWidth = 8f;
    
    [Header("[FLAMMABLE] SETTINGS : ")]
    [SerializeField] private Color _outlineFlammableColor = new Color(1f, 0f, 0f, 0.8f);
    [SerializeField] private float _outlineFLammableWidth = 8f;

    [Header("[PAUSE MENU] SETTINGS : ")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private bool paused = false;

    // * --- Public references --- * //
    public Color OutlineColor => _outlineColor;
    public float OutlineWidth => _outlineWidth;


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

    private void Start()
    {
        _pauseMenu.SetActive(false); /* To be sure pause Menu isn't active on Start */
    }


    private void OnEnable()
    {
        _playerInteractions = _playerReferences.PlayerInteractions;
        _playerLaunchMatches = _playerReferences.PlayerLaunchMatches;
        _playerInsanity = _playerReferences.PlayerInsanity;


        /* - Events - */
        _playerInteractions.OnFocusInteractable += ChangePointerColor;
        _playerLaunchMatches.OnChangeNumberOfMatches += UpdateNumberOfMatchesIndicator;
        _playerLaunchMatches.OnForceChange += UpdateForceIndicator;
        _playerInsanity.OnInsanityChange += UpdateInsanityIndicator;

        _playerReferences.Controls.OnEscapeClick += TogglePauseMenuPanel; 


        /* - First Update - */
        InitalConfigForceIndicator();
        UpdateNumberOfMatchesIndicator(_playerLaunchMatches.NumberOfMatches);
        UpdateForceIndicator(_playerLaunchMatches.Force);
        UpdateInsanityIndicator(_playerInsanity.InsanityLvl);

    }

    private void OnDisable()
    {
        /* - Events - */
        _playerInteractions.OnFocusInteractable -= ChangePointerColor;
        _playerLaunchMatches.OnChangeNumberOfMatches -= UpdateNumberOfMatchesIndicator;
        _playerLaunchMatches.OnForceChange -= UpdateForceIndicator;
        _playerInsanity.OnInsanityChange -= UpdateInsanityIndicator;

        _playerReferences.Controls.OnEscapeClick -= TogglePauseMenuPanel;

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
    private void UpdateForceIndicator(float force) => _forceSliderTest.fillAmount = Mathf.InverseLerp(1f, 10f, force); //_forceSlider.value = force;
    private void UpdateInsanityIndicator(int insanity) => _bpmCount.text = insanity.ToString();

    /* - Public method to change sensitivity of the pointer -> maybe to move on other script "UI" made by luc dedicated to Pause Menu ? | Or keep it in this general UI Manager ? | Or made a "Game Settings" Script ? - */
    public void OnPointerSensitivityChanged(float value)
    {
        _playerReferences.PointerSensitivity = value;
    }

    // * --- Methods for Inspection panel --- * //

    public bool IsInspectionPanelOpen => _inspectionPanel.activeSelf;

    public void ToggleInspectionPanel(InspectableObjectData data)
    {
        bool isActive = !_inspectionPanel.activeSelf;
        _inspectionPanel.SetActive(isActive);

        if (isActive)
        {
            _textNameObject.text = data.Name;
            _textDescription.text = data.Description;
        }
    }

    // * --- Methods for Pause Menu Panel --- *

    public void TogglePauseMenuPanel()
    {
        if (!paused)
        {
            paused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;

            _playerReferences.PlayerMovements.CanMove(false);
            _playerReferences.PlayerMovements.CanLook(false);

            _playerReferences.Controls.CanThrow = false; 
        }
        else
        {
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;

            _playerReferences.PlayerMovements.CanMove(true);
            _playerReferences.PlayerMovements.CanLook(true);

            _playerReferences.Controls.CanThrow = true;

        }
    }

    // * --- General Methods --- * //

}
