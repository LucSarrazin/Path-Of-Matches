using System.Collections;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private PlayerReferences _playerReferences;
    //[SerializeField] private CapsuleCollider _capsuleCollider;
    private CharacterController _characterController;

    private Vector3 _slopeNormal = Vector3.up;
    private bool _isGrounded = true;

    private bool _isStepping;

    private Vector2 _moveInputs;
    private Rigidbody _rigidbody;
    private float _currentSpeed;
    private bool _canMove;
    private bool _canLook;

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

        //_rigidbody = _playerReferences.Rigidbody;
        _characterController = _playerReferences.CharacterController;
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
        MovePlayer(); /* Move here, because of rigidbody replacement by character controller component*/ 
    }

    private void FixedUpdate()
    {
        //GroundCheck();
        //StepOffset();
        //MovePlayer();
    }

    ///* --- Method : GROUND CHECKER, SphereCast type --- */

    //private void GroundCheck()
    //{
    //    Vector3 pivotFootPosition = transform.position - Vector3.up * (_capsuleCollider.height / 2f);
    //    Vector3 origin = pivotFootPosition + Vector3.up * _playerReferences.GroundCheckRadius;

    //    if (Physics.SphereCast(origin, _playerReferences.GroundCheckRadius, Vector3.down,
    //        out RaycastHit hit, _playerReferences.GroundCheckDistance, _playerReferences.GroundLayer))
    //    {
    //        _isGrounded = true;
    //        _slopeNormal = hit.normal; // normale's value you touch, to keep 
    //    }
    //    else
    //    {
    //        _isGrounded = false;
    //        _slopeNormal = Vector3.up; // default
    //    }
    //}


    //private void OnDrawGizmos() /* Editor Scripting : Method to draw groundChecker in Editor */
    //{
    //    if (_capsuleCollider == null) return;

    //    /* - Draw GroundChecker - */

    //    Vector3 pivotFootPosition = transform.position - Vector3.up * (_capsuleCollider.height / 2f);
    //    Vector3 origin = pivotFootPosition + Vector3.up * _playerReferences.GroundCheckRadius;

    //    // Simule le SphereCast uniquement pour le Gizmo
    //    bool grounded = Physics.SphereCast(origin, _playerReferences.GroundCheckRadius, Vector3.down,
    //        out _, _playerReferences.GroundCheckDistance, _playerReferences.GroundLayer);

    //    Gizmos.color = grounded ? Color.green : Color.red;
    //    Gizmos.DrawWireSphere(origin + Vector3.down * _playerReferences.GroundCheckDistance, _playerReferences.GroundCheckRadius);

    //    /* - Draw StepOffSet Checker - */

    //    Vector3 moveDirection = (transform.right * _moveInputs.x
    //                           + transform.forward * _moveInputs.y).normalized;

    //    // En �diteur, si pas de direction, on utilise forward par d�faut
    //    if (moveDirection == Vector3.zero) moveDirection = transform.forward;

    //    // --- Raycast BAS ---
    //    Vector3 rayLowOrigin = pivotFootPosition + Vector3.up * 0.05f;
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawRay(rayLowOrigin, moveDirection * _playerReferences.StepCheckDistance);

    //    // --- Raycast HAUT ---
    //    Vector3 rayHighOrigin = pivotFootPosition + Vector3.up * (_playerReferences.StepHeight + 0.05f);
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawRay(rayHighOrigin, moveDirection * _playerReferences.StepCheckDistance);

    //    // --- Visualisation de la hauteur de marche ---
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(pivotFootPosition + Vector3.up * 1f, pivotFootPosition + Vector3.up * (_playerReferences.StepHeight + 1f));
    //}

    //private bool IsOnSlope()
    //{
    //    float angle = Vector3.Angle(Vector3.up, _slopeNormal);
    //    if (angle > 0.1f && angle < _playerReferences.MaxSlopeAngle)
    //    {
    //        return true;
    //    }
    //    return false;

    //}

    /* --- Method : StepOffset System to allow Upstairs --- */

    //private void StepOffset()
    //{
    //    if (!_isGrounded || _moveInputs.sqrMagnitude < 0.1f)
    //    {
    //        _isStepping = false;
    //        return;
    //    }

    //    Vector3 pivotFootPosition = transform.position - Vector3.up * (_capsuleCollider.height / 2f);

    //    Vector3 moveDirection = (transform.right * _moveInputs.x + transform.forward * _moveInputs.y).normalized;

    //    // --- Raycast BAS ---
    //    Vector3 rayLowOrigin = pivotFootPosition + Vector3.up * 0.05f;

    //    bool hitLow = Physics.Raycast(
    //        rayLowOrigin,
    //        moveDirection,
    //        out RaycastHit hitLowInfo,
    //        _playerReferences.StepCheckDistance,
    //        _playerReferences.GroundLayer
    //    );

    //    if (!hitLow)
    //    {
    //        _isStepping = false;
    //        return;
    //    }

    //    // --- Raycast HAUT ---
    //    Vector3 rayHighOrigin = pivotFootPosition + Vector3.up * (_playerReferences.StepHeight + 0.05f);

    //    bool hitHigh = Physics.Raycast(
    //        rayHighOrigin,
    //        moveDirection,
    //        _playerReferences.StepCheckDistance,
    //        _playerReferences.GroundLayer
    //    );

    //    // --- Condition de step ---
    //    if (!hitHigh && !_isStepping)
    //    {
    //        _isStepping = true;

    //        //// petit d�placement vertical seulement (simple et efficace)
    //        //_rigidbody.MovePosition(
    //        //    _rigidbody.position + Vector3.up * (_playerReferences.StepHeight * 0.5f)

    //        _rigidbody.linearVelocity = new Vector3(
    //_rigidbody.linearVelocity.x,
    //2f,
    //_rigidbody.linearVelocity.z

    //        );
    //    }
    //}

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

    //private void MovePlayer()
    //{
    //    if (!_canMove) { return; }

    //    Vector3 move = transform.right * _moveInputs.x + transform.forward * _moveInputs.y;

    //    if (move.sqrMagnitude < 0.01f)
    //    {
    //        _rigidbody.linearVelocity = new Vector3(0f, _rigidbody.linearVelocity.y, 0f);
    //        return;
    //    }

    //    move.Normalize();

    //    if (_isGrounded && IsOnSlope())
    //    {
    //        // projetter d�placement player sur le plan de la pente
    //        move = Vector3.ProjectOnPlane(move, _slopeNormal).normalized;
    //    }
    //    Vector3 velocity = move * _currentSpeed;
    //    velocity.y = _rigidbody.linearVelocity.y;

    //    if (_isGrounded && IsOnSlope())
    //    {
    //        velocity.y = -1f; // force to stick on ground and avoid slide
    //    }

    //    _rigidbody.linearVelocity = velocity;
    //    _rigidbody.angularVelocity = Vector3.zero;
    //}

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

    private void LookPlayer()
    {

        if (!_canLook) { return; }

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
