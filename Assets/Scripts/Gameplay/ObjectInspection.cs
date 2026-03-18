using System;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ObjectInspection : Inspectable
{
    private InputSystem_Actions actions;
    private Vector2 _mousePosition;
    private Vector2 _screenSize;
    [SerializeField] private PlayerReferences playerReferences;
    [SerializeField] private TextMeshProUGUI textNameObject;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject UIInspection;
    [SerializeField] private bool isDragging = false;
    [SerializeField] private bool flipFlop = false;
    [SerializeField] private float force;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private AnimationCurve slerpCurve;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationReturnSpeed = 5f;
    [SerializeField] private Collider collider;
    [SerializeField] private string description;
    [SerializeField] private GameObject light;
    [SerializeField] private Light pointLightMatches;
    private PlayerInputActions playerInputActions;
    private Camera playerCamera;

    private void DragOnstarted(InputAction.CallbackContext obj)
    {
        if (flipFlop == true)
        {
            isDragging = true;
        }
    }

    private void DragOncancel(InputAction.CallbackContext obj)
    {
        if (flipFlop == true)
        {
            isDragging = false;
        }
    }

    private void OnEnable()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = Camera.main.transform.position + Camera.main.transform.forward * 0.8f;
        startPosition = transform.position;
        playerCamera = Camera.main;
        playerInputActions.Player.SwitchMatch.performed += DragOnstarted;
        playerInputActions.Player.SwitchMatch.canceled += DragOncancel;
    }

    // Update is called once per frame
    void Update()
    {
        offset = Camera.main.transform.position + Camera.main.transform.forward * 0.8f;
        _mousePosition = Mouse.current.position.ReadValue();
        _screenSize = new Vector2(_mousePosition.x / Screen.width - 0.5f, _mousePosition.y / Screen.height - 0.5f);

        if (flipFlop == true)
        {
            transform.position = Vector3.Lerp(transform.position, offset + playerReferences.transform.right * -1f * 0.4f, moveSpeed * Time.unscaledDeltaTime);
        }

        if (isDragging == true)
        {
            transform.position = Vector3.Lerp(transform.position, offset + playerReferences.transform.right * -1f * 0.4f, moveSpeed * Time.unscaledDeltaTime);
            transform.Rotate(_screenSize.x * force * Time.unscaledDeltaTime * Vector3.up, Space.World);
            transform.Rotate(_screenSize.y * force * Time.unscaledDeltaTime * Vector3.right, Space.World);
        }
        else if(flipFlop == false)
        {
            transform.position = Vector3.Lerp(transform.position,startPosition,moveSpeed * Time.unscaledDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationReturnSpeed * Time.unscaledDeltaTime);
        }
    }
    
    
    public override void Interact()
    {
        if (flipFlop != true)
        {
            Debug.Log("Ouverture");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            flipFlop = true;
            //transform.position = offset + playerReferences.transform.right * -1f * 0.4f;
            pointLightMatches.intensity = 0.3f;
            light.SetActive(true);
            textNameObject.text = gameObject.name;
            textDescription.text = description;
            UI.SetActive(false);
            UIInspection.SetActive(true);
            offset = Camera.main.transform.position + Camera.main.transform.forward * 0.8f;
            ((BoxCollider)collider).size = new Vector3(10f, 10f, 10f);    
            Time.timeScale = 0f;
        }
        else
        {
            Debug.Log("Fermeture"); 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            flipFlop = false;
            pointLightMatches.intensity = 10f;
            light.SetActive(true);
            textNameObject.text = null;
            textDescription.text = null;
            UI.SetActive(true);
            playerInputActions.Player.Escape.performed += UI.GetComponent<PauseMenuButton>().EscapeOnperformed;
            UIInspection.SetActive(false);
            ((BoxCollider)collider).size = new Vector3(1f, 1f, 1f); 
            Time.timeScale = 1f;
        }
    }

    public override void OnFocus()
    {

    }

    public override void LoseFocus()
    {

    }
}
