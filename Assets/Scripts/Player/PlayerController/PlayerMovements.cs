using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private PlayerReferences _playerReferences;
    private Vector2 _moveInputs;
    private Rigidbody _rigidbody;
    private bool _canMove;
    private float _currentSpeed;

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

        /*TEST*/
        _pointerSensitivity = _playerReferences.PointerSensitivity;
    }

    private void Update()
    {
        LookPlayer(); 
    }

    private void FixedUpdate()
    {
        if (!_canMove) { return; }
        MovePlayer();
    }

    /* MOVE : */

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
    }

    /* LOOK : */

    public void SetLookInputs(Vector2 look)
    {
        _lookInputs = look;
    }

    private void LookPlayer()
    {
        //SetLookInputs(_playerReferences.Controls.LookInputs);

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
