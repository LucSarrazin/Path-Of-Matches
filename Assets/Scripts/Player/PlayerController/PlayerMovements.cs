using System.Collections;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private CapsuleCollider _capsuleCollider;

    private Vector3 _slopeNormal = Vector3.up;
    private bool _isGrounded = true;

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

    private IEnumerator Start()
    {
        /* Method to set view on Start */
        _xRotation = 0f;
        _playerReferences.Head.localRotation = Quaternion.identity;

        /*Method to lock cursor on screen*/
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yield return null; /* Wait one frame to avoid delta error */

        _lookInputs = Vector2.zero;
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
    }

    private void FixedUpdate()
    {
        GroundCheck();
        MovePlayer();
    }

    /* --- Method : GROUND CHECKER, SphereCast type --- */

    private void GroundCheck() 
    {
        Vector3 pivotFootPosition = transform.position - Vector3.up * (_capsuleCollider.height /2f);
        Vector3 origin = pivotFootPosition + Vector3.up * _playerReferences.GroundCheckRadius;

        if (Physics.SphereCast(origin, _playerReferences.GroundCheckRadius, Vector3.down,
            out RaycastHit hit, _playerReferences.GroundCheckDistance, _playerReferences.GroundLayer))
        {
            _isGrounded = true;
            _slopeNormal = hit.normal; // normale's value you touch, to keep 
        }else
        {
            _isGrounded = false;
            _slopeNormal = Vector3.up; // default
        }
    }


    private void OnDrawGizmos() /* Editor Scripting : Method to draw groundChecker in Editor */
    {
        if (_capsuleCollider == null) return;

        Vector3 pivotFootPosition = transform.position - Vector3.up * (_capsuleCollider.height / 2f);
        Vector3 origin = pivotFootPosition + Vector3.up * _playerReferences.GroundCheckRadius;

        // Simule le SphereCast uniquement pour le Gizmo
        bool grounded = Physics.SphereCast(origin, _playerReferences.GroundCheckRadius, Vector3.down, 
            out _, _playerReferences.GroundCheckDistance, _playerReferences.GroundLayer);

        Gizmos.color = grounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(origin + Vector3.down * _playerReferences.GroundCheckDistance,
                              _playerReferences.GroundCheckRadius);
    }

    private bool IsOnSlope()
    {
        float angle = Vector3.Angle(Vector3.up, _slopeNormal);
        if (angle > 0.1f && angle < _playerReferences.MaxSlopeAngle)
        {
            return true;
        }
        return false;

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

        if (_isGrounded && IsOnSlope())
        {
            // projetter déplacement player sur le plan de la pente
            move = Vector3.ProjectOnPlane(move, _slopeNormal).normalized;
        }
        Vector3 velocity = move * _currentSpeed;
        velocity.y = _rigidbody.linearVelocity.y;

        if (_isGrounded && IsOnSlope())
        {
            velocity.y = -1f; // force to stick on ground and avoid slide
        }

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

    private void UpdatePointerSensitivity(float sensitivity)
    {
        _pointerSensitivity = sensitivity;
    }

    private void LookPlayer()
    {
        Debug.Log($"CanLook: {_canLook}"); // à retirer après debug

        if (!_canLook) { return; }

        float pointerX = _lookInputs.x * _pointerSensitivity/* * Time.deltaTime*/;
        float pointerY = _lookInputs.y * _pointerSensitivity /* * Time.deltaTime*/;

        /* Horizontal rotation : */
        _playerReferences.transform.Rotate(Vector3.up * pointerX);

        /* Vertical rotation : */
        _xRotation -= pointerY; /*Inverse*/
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); /* to avoid absolute flip */

        _playerReferences.Head.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }


}
