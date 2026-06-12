using System.Collections;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private PlayerReferences _playerReferences;
    private CharacterController _characterController;
    private Vector2 _moveInputs;
    private float _currentSpeed;
    public float CurrentSpeed => _currentSpeed;

    private bool _canMove;
    private bool _canLook = false;

    private Vector2 _lookInputs;
    private float _pointerSensitivity;
    private float _xRotation = 0f;

    private Vector3 _velocity; /* For gravity */


    private void Awake()
    {
        /* [SAFETY] "Get Components" to load scripts, if they aren't connected in inspector : */

        if (_playerReferences == null)
        {
            _playerReferences = GetComponentInParent<PlayerReferences>();
            Debug.Log($" - GO : {this} -> script 'PlayerReferences' charged by GetComponent.");
        }

        _characterController = _playerReferences.CharacterController;
        _pointerSensitivity = _playerReferences.PointerSensitivity;
    }

    private IEnumerator Start()
    {
        //GameEvents.OnAutoSaveRequested?.Invoke(_playerReferences.transform);
        yield return null; /* Wait one frame to avoid delta error */

        /*Method to lock cursor on screen*/
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _canLook = true;
    }

    private void OnEnable()
    {
        _canLook = false;

        /* - Events */
        _playerReferences.OnPointerSensitivityChanged += UpdatePointerSensitivity;
    }

    private void OnDisable()
    {
        /* - Events */
        _playerReferences.OnPointerSensitivityChanged -= UpdatePointerSensitivity;
    }

    private void Update()
    {
        LookPlayer();
        MovePlayer(); /* Move here and not in FixedUpdate, because of rigidbody replacement by character controller component is using Update only*/ 
    }

    /* --- Method : MOVE --- */

    public void SetSpeed(float speed)
    {
        _currentSpeed = speed;
    }

    public void SetMoveInputs(Vector2 input)
    {
        _moveInputs = input;
    }


    public void CanMove(bool enable)
    {
        _canMove = enable;
    }

    private void MovePlayer()
    {
        if (!_canMove) return; 

        /* - Manual gravity - because Character controler replace classical rigidbody call */

        if (_characterController.isGrounded && _velocity.y > 0f)
        {
            _velocity.y = -2f; /* force stick on ground */
        }
        else
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime; 
        }

        Vector3 move = transform.right * _moveInputs.x + transform.forward * _moveInputs.y;

        _characterController.Move((move * _currentSpeed + _velocity) * Time.deltaTime);
    }

    /* --- Method : LOOK --- */

    public void CanLook(bool enable)
    { _canLook = enable; }

    public void SetLookInputs(Vector2 look)
    {
        _lookInputs = look;
    }

    private void UpdatePointerSensitivity(float sensitivity)
    {
        _pointerSensitivity = sensitivity;
    }

    public void SetXRotation(float value) /*Used in Load Save System to avoid mistake on rotation*/
    {
        _xRotation = Mathf.Clamp(value, -90f, 90f); // on clamp aussi ici par sécurité
    }

    private void LookPlayer()
    {
        if (!_canLook) 
        { 
            return; 
        }

        float pointerX = _pointerSensitivity * _lookInputs.x;
        float pointerY = _pointerSensitivity *_lookInputs.y;

        /* Horizontal rotation : */
        _playerReferences.transform.Rotate(Vector3.up * pointerX);

        /* Vertical rotation : */
        _xRotation -= pointerY; /*Inverse*/
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); /* to avoid absolute flip */

        _playerReferences.Head.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }


}
