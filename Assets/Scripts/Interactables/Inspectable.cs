using UnityEngine;
using UnityEngine.InputSystem;

public class Inspectable : Interactable
{

    [Header("SETTINGS :")]
    [SerializeField] private bool _freezeMovement = true;
    [SerializeField] private bool _freezeRotationLook = true;
    [Tooltip("Speed of the object to move on panel")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationReturnSpeed = 5f;
    [Tooltip("Rotation force of the object inspectable")]
    [SerializeField] private float _force;

    [Header("References")]
    [SerializeField] private InspectableObjectData _data;
    [SerializeField] private PlayerReferences _playerReferences;

    public override bool FreezeMovement => _freezeMovement;
    public override bool FreezeRotationLook => _freezeRotationLook;

    /* -- Display Object variables -- */

    /*[SerializeField] */
    private bool _flipFlop;
    private bool _isDragging;
    private Vector2 _screenSize;
    private Vector3 _startPosition;
    private Vector3 _offset;
    private BoxCollider _collider;


    private void Awake()
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
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            transform.Rotate(mouseDelta.x * _force * Time.unscaledDeltaTime * Vector3.up, Space.World);
            transform.Rotate(mouseDelta.y * _force * Time.unscaledDeltaTime * Vector3.right, Space.World);
        }

        else if (!_flipFlop)
        {
            transform.position = Vector3.Lerp(transform.position, _startPosition, _moveSpeed * Time.unscaledDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, _rotationReturnSpeed * Time.unscaledDeltaTime);

        }
    }

    public void Open()
    {
        _startPosition = transform.position;
        _flipFlop = true;
        UIManager.Instance.ToggleInspectionPanel(_data);
    }

    public void Close()
    {
        _flipFlop = false;
        _isDragging = false;
        UIManager.Instance.ToggleInspectionPanel(_data);
    }

    // Interact() pour compatibilité
    public override void Interact() { }

    public void SetDragging(bool value)
    {
        _isDragging = value;
    }

}
