using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [Header("[PLAYER] MOVEMENTS VARIABLES :")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _pointerSensitivity = 100f;

    [Header("[PLAYER] INTERACTION VARIABLES")]
    [SerializeField] private LayerMask _interactibleLayerMask;
    [SerializeField] private float _checkDistance = 15f; 

    [Header("[PLAYER] GENERAL COMPONENTS :")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _head;
    [SerializeField] private Camera _viewCamera;
    [SerializeField] private PlayerControls _controls;
    [SerializeField] private PlayerMovements _playerMovements;
    [SerializeField] private PlayerInteractions _playerInteractions;


    #region PUBLIC REFERENCES : 

    // VARIABLES //

    public float WalkSpeed { get => _walkSpeed; }
    public float RunSpeed { get => _runSpeed; }
    public float PointerSensitivity { get => _pointerSensitivity; }
    public float CheckDistance { get => _checkDistance; }

    // COMPONENTS //

    public Rigidbody Rigidbody { get => _rigidbody; }
    public Transform Head { get => _head; }
    public LayerMask InteractibleLayer { get => _interactibleLayerMask; }

    // SCRIPTS //

    public PlayerControls Controls { get => _controls; }
    public PlayerMovements PlayerMovements { get => _playerMovements; }
    public PlayerInteractions PlayerInteractions { get => _playerInteractions; }
    public Camera PlayerViewCamera { get => _viewCamera; }

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
