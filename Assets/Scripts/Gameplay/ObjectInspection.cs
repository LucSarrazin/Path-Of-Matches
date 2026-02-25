using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectInspection : Inspectable
{
    private InputSystem_Actions actions;
    private Vector2 _mousePosition;
    private Vector2 _screenSize;
    private bool isDragging = false;
    private bool flipFlop = false;
    [SerializeField] private float force;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private AnimationCurve slerpCurve;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationReturnSpeed = 5f;
    [SerializeField] private Collider collider;
    private Camera playerCamera;

    public override bool FreezeMovement => throw new NotImplementedException();

    public override bool FreezeRotationLook => throw new NotImplementedException();

    private void OnEnable()
    {
        actions = new InputSystem_Actions();
        actions.Player.Enable();
        //actions.Player.Attack.started += AttackOnstarted;
        //actions.Player.Attack.canceled += AttackOncancel;
    }

    /*private void AttackOnstarted(InputAction.CallbackContext obj)
    {
        isDragging = true;
    }

    private void AttackOncancel(InputAction.CallbackContext obj)
    {
        isDragging = false;
    }*/

    private void OnDisable()
    {
        actions.Player.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 0.5f);
        startPosition = transform.position;
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 0.5f);
        _mousePosition = Mouse.current.position.ReadValue();
        _screenSize = new Vector2(_mousePosition.x / Screen.width - 0.5f, _mousePosition.y / Screen.height - 0.5f);

        if (isDragging == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            transform.position = Vector3.Lerp(transform.position,offset, moveSpeed * Time.deltaTime);
            transform.Rotate(_screenSize.x * force * Time.deltaTime * Vector3.up, Space.World);
            transform.Rotate(_screenSize.y * force * Time.deltaTime * Vector3.left, Space.World);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            transform.position = Vector3.Lerp(transform.position,startPosition,moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationReturnSpeed * Time.deltaTime);
        }
    }
    
    
    public override void Interact()
    {
        if (!flipFlop)
        {
            flipFlop = true;
            isDragging = true;
            offset = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 0.5f);        }
        else
        {
            flipFlop = false;
            isDragging = false;
        }
    }

    public override void OnFocus()
    {
        
    }

    public override void LoseFocus()
    {
        
    }
}
