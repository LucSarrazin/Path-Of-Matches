using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private PlayerReferences _playerReferences;
    private Vector2 _moveInputs;
    private Rigidbody _rigidbody;
    private bool _canMove;

    private float _currentSpeed;

    private void Awake()
    {
        /* [SAFETY] "Get Components" to load scripts, if they aren't connected in inspector : */

        if (_playerReferences == null)
        {
            _playerReferences = GetComponentInParent<PlayerReferences>();
            Debug.Log($" - GO : {this} -> script 'PlayerReferences' charged by GetComponent.");
        }

        _rigidbody = _playerReferences.Rigidbody;

    }

    private void FixedUpdate()
    {
        if (!_canMove) { return; }
        MovePlayer();
    }


    /* MOVE */

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
}
