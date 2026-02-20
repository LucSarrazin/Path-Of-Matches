using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private PlayerReferences _playerReferences;
    private Vector2 _moveInputs;
    private Rigidbody _rigidbody;
    private float _currentSpeed;
    private bool _canMove;
    private bool _canLook;

    private Vector2 _lookInputs;
    private float _pointerSensitivity;
    private float _xRotation = 0f;


    private void Awake()
    {
        /* [SAFETY] "Get Components" to load scripts, if they aren't connected in inspector : */

        if (_playerReferences == null)
        {
            _playerReferences = GetComponentInParent<PlayerReferences>();
            Debug.Log($" - GO : {this} -> script 'PlayerReferences' charged by GetComponent.");
        }

        _rigidbody = _playerReferences.Rigidbody;
        _pointerSensitivity = _playerReferences.PointerSensitivity;
    }

    private void Start()
    {
        /* Method to set view on Start */
        _xRotation = 0f;
        _playerReferences.Head.localRotation = Quaternion.identity;

        /*Method to lock cursor on screen*/
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        LookPlayer();
    }

    private void FixedUpdate()
    {
        MovePlayer();
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
        if (!_canMove) { return; }

        Vector3 move = transform.right * _moveInputs.x + transform.forward * _moveInputs.y;

        if (move.sqrMagnitude < 0.01f)
        {
            _rigidbody.linearVelocity = new Vector3(0f, _rigidbody.linearVelocity.y, 0f);
            return;
        }

        move.Normalize();

        Vector3 velocity = move * _currentSpeed;
        velocity.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = velocity;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    /* --- Method : LOOK --- */

    public void CanLook(bool enable)
    { _canLook = enable; }

    public void SetLookInputs(Vector2 look)
    {
        _lookInputs = look;
    }

    private void LookPlayer()
    {
        if (!_canLook) { return; }

        float pointerX = _lookInputs.x * _pointerSensitivity * Time.deltaTime;
        float pointerY = _lookInputs.y * _pointerSensitivity * Time.deltaTime;

        /* Horizontal rotation : */
        _playerReferences.transform.Rotate(Vector3.up * pointerX);

        /* Vertical rotation : */
        _xRotation -= pointerY; /*Inverse*/
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); /* to avoid absolute flip */

        _playerReferences.Head.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }


}
