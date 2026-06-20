using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Inspectable : Interactable
{
    [Header("[INSPECTABLE] GENERAL SETTINGS ")]
    //[Header("Player movements freeze settings : ")]
    [SerializeField] private bool _freezeMovement = true;
    [SerializeField] private bool _freezeRotationLook = true;
    [Header("Object behaviour in UI Inspection Panel: ")]
    [Tooltip("Speed of the object to move on panel, test/read-only : please edit in script after test, to keep harmony")]
    [SerializeField] private float _moveSpeed = 5f;
    [Tooltip("Speed rotation of the object to quit panel, test/read-only : please edit in script after test, to keep harmony")]
    [SerializeField] private float _rotationReturnSpeed = 5f;
    [Tooltip("Rotation force of the object inspectable, test/read-only : please edit in script after test, to keep harmony")]
    [SerializeField] private float _force = 50;

    [Header("DATAS :")]
    [Tooltip("Datas to display on UI inspectable panel")]
    [SerializeField] private InspectableObjectData _data;
    [SerializeField] private UnityEvent _OpenPage,_ClosingPage;

    /* --- Private variables --- */

    public override bool FreezeMovement => _freezeMovement;
    public override bool FreezeRotationLook => _freezeRotationLook;

    /* -- Display Object variables -- */
    private bool _flipFlop;
    private bool _isDragging;
    private Vector2 _screenSize;
    private Vector3 _startPosition;
    private Vector3 _offset;
    private BoxCollider _collider;


    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<BoxCollider>();
        _startPosition = transform.position;

    }

    private void Update()
    {
        _offset = Camera.main.transform.position + Camera.main.transform.forward * 0.8f;

        if (_flipFlop)
        {
            transform.position = Vector3.Lerp(transform.position, _offset + _playerReferences.transform.right * -1f * 0.4f, _moveSpeed * Time.unscaledDeltaTime);
        }

        if (_isDragging)
        {
            // Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            // float rotationX = mouseDelta.y * _force * Time.unscaledDeltaTime;
            // float rotationY = -mouseDelta.x * _force * Time.unscaledDeltaTime;
            //
            // Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
            // transform.rotation = rotation * transform.rotation;
            
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            float rotationX = mouseDelta.y * _force * Time.unscaledDeltaTime;
            float rotationY = -mouseDelta.x * _force * Time.unscaledDeltaTime;

            transform.Rotate(Camera.main.transform.up, rotationY, Space.World);
            transform.Rotate(Camera.main.transform.right, rotationX, Space.World);
        }

        else if (!_flipFlop)
        {
            transform.position = Vector3.Lerp(transform.position, _startPosition, _moveSpeed * Time.unscaledDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, _rotationReturnSpeed * Time.unscaledDeltaTime);

        }
    }

    public void Open()
    {
        _playerReferences.PlayerMovements.SetLookInputs(Vector2.zero);
        _flipFlop = true;
        _collider.enabled = false;

        _playerReferences.PointLightMatches.intensity = 0.3f;
        _playerReferences.Light.SetActive(true);

        UIManager.Instance.ToggleInspectionPanel(_data);
        Interact();
        _OpenPage?.Invoke();
    }

    public void Close()
    {
        _flipFlop = false;
        _isDragging = false;
        _playerReferences.PointLightMatches.intensity = 10f;
        _playerReferences.Light.SetActive(true);

        UIManager.Instance.ToggleInspectionPanel(_data);
        _collider.enabled = true;
        _ClosingPage?.Invoke();
    }

    // Interact() pour compatibilit�
    public override void Interact() { }

    public void SetDragging(bool value)
    {
        _isDragging = value;
    }

}
