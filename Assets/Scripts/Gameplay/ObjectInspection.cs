using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectInspection : Inspectable
{
    private InputSystem_Actions actions;
    private Vector2 _mousePosition;
    private Vector2 _screenSize;
    [SerializeField] private bool isDragging = false;
    [SerializeField] private bool flipFlop = false;
    [SerializeField] private float force;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private AnimationCurve slerpCurve;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationReturnSpeed = 5f;
    [SerializeField] private Collider collider;
    private Camera playerCamera;

    /*private void AttackOnstarted(InputAction.CallbackContext obj)
    {
        isDragging = true;
    }

    private void AttackOncancel(InputAction.CallbackContext obj)
    {
        isDragging = false;
    }*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = Camera.main.transform.position + Camera.main.transform.forward * 0.8f;
        startPosition = transform.position;
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        offset = Camera.main.transform.position + Camera.main.transform.forward * 0.8f;
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
        Debug.Log("Interact appelé depuis : " + new System.Diagnostics.StackTrace());
        if (flipFlop != true)
        {
            Debug.Log("Ouverture");
            isDragging = true;
            flipFlop = true;
            offset = Camera.main.transform.position + Camera.main.transform.forward * 0.8f;
        }
        else
        {
            Debug.Log("Fermeture");
            isDragging = false;
            flipFlop = false;
        }
    }

    public override void OnFocus()
    {
        
    }

    public override void LoseFocus()
    {
        
    }
}
