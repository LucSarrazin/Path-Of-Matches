using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [Header("[PLAYER] MOVEMENTS VARIABLES :")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _pointerSensitivity = 100f;

    [Header("[PLAYER] GENERAL COMPONENTS :")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _head;
    [SerializeField] private PlayerControls _controls;
    [SerializeField] private PlayerMovements _playerMovements;

    #region PUBLIC REFERENCES : 

    // VARIABLES //

    public float WalkSpeed { get => _walkSpeed; }
    public float PointerSensitivity { get => _pointerSensitivity; }

    // COMPONENTS //

    public Rigidbody Rigidbody { get => _rigidbody; }
    public Transform Head { get => _head; }

    // SCRIPTS //

    public PlayerControls Controls { get => _controls; }
    public PlayerMovements PlayerMovements { get => _playerMovements; }

    #endregion

    private void Awake()
    {
        /* [SAFETY] "Get Components" to load scripts, if they aren't connected in inspector : */
        
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
            Debug.Log($" - GO : {this} -> script 'Rigidbody' charged by GetComponent.");
        }

        if (_playerMovements == null)
        {
            _playerMovements = GetComponentInChildren<PlayerMovements>();
            Debug.Log($" - GO : {this} -> script 'PlayerMovement' charged by GetComponent.");
        }

        if (_controls == null)
        {
            _controls = GetComponentInChildren<PlayerControls>();
            Debug.Log($" - GO : {this} -> script 'PlayerControls' charged by GetComponent.");
        }


    }
}
