using System;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [Header("[PLAYER] MOVEMENTS - VIEW VARIABLES :")]
    [SerializeField] private float _pointerSensitivity = 100f;

    [Header("[PLAYER] MOVEMENTS - SPEED VARIABLES :")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    //** -- Section dedicated to slopes and stairs behaviour -- ** //

    [Header("[PLAYER] MOVEMENTS - GROUND CHECKER SYSTEM :")]
    /* Radius needs to always be lower than the capsule collider radius of the character, if not it can detect walls ! */
    [SerializeField] private float _groundCheckRadius = 0.3f;
    [SerializeField] private float _groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    [Header("[PLAYER] MOVEMENTS - SLOPE AND STEP OFFSET SYSTEM :")]
    [Tooltip("Max angle between ground and slope - Angle max entre la surface du sol et celle de la pente ")]
    [SerializeField] private float _maxSlopeAngle = 45f;
    [SerializeField] private float _stepHeight = 0.4f;
    [SerializeField] private float _stepCheckDistance = 0.5f;

    //** -- Section dedicated to interaction behaviour -- ** //

    [Header("[PLAYER] INTERACTION SETTINGS")]
    [SerializeField] private LayerMask _interactibleLayerMask;
    [Tooltip("Check distance of the raycast view !")]
    [SerializeField] private float _checkDistance = 15f;
    [SerializeField] private GameObject _interactibleFocusSprite;

    //** -- Section dedicated to general script references -- ** //

    [Header("[PLAYER] GENERAL COMPONENTS :")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _head;
    [SerializeField] private Camera _viewCamera;

    [Header("[PLAYER] MATCHES COMPONENTS :")]
    [SerializeField] private GameObject _light;
    [SerializeField] private Light _pointLightMatches;

    [Header("[PLAYER] CONTROLS SCRIPTS :")]
    [SerializeField] private PlayerControllerSM _playerControllerSM; /* Ref to StateMachine */ 
    [SerializeField] private PlayerControls _controls;
    [SerializeField] private PlayerMovements _playerMovements;
    [SerializeField] private PlayerInteractions _playerInteractions;
    [SerializeField] private PlayerSwitchMatches _playerSwitchMatches;
    [SerializeField] private PlayerLaunchMatches _playerLaunchMatches;
    [SerializeField] private Insanity _playerInsanity;

    public Action<float> OnPointerSensitivityChanged; 


    #region PUBLIC REFERENCES : 

    // VARIABLES //

    public float WalkSpeed { get => _walkSpeed; }
    public float RunSpeed { get => _runSpeed; }
    public float PointerSensitivity {
        get => _pointerSensitivity;
        set
        {
            if (_pointerSensitivity == value) return;

            _pointerSensitivity = value;
            OnPointerSensitivityChanged?.Invoke(value);
        }
    }

    public float GroundCheckRadius => _groundCheckRadius;
    public float GroundCheckDistance => _groundCheckDistance;
    public LayerMask GroundLayer => _groundLayer;

    public float MaxSlopeAngle => _maxSlopeAngle;
    public float StepHeight => _stepHeight;
    public float StepCheckDistance => _stepCheckDistance;

    public float CheckDistance { get => _checkDistance; } /* Pointer view raycast */

    // COMPONENTS //

    public Rigidbody Rigidbody { get => _rigidbody; }
    public Transform Head { get => _head; }
    public LayerMask InteractibleLayer { get => _interactibleLayerMask; }
    public GameObject InteractibleFocusSprite {  get => _interactibleFocusSprite; }
    public GameObject Light => _light; 
    public Light PointLightMatches => _pointLightMatches;

    // SCRIPTS //

    public PlayerControllerSM PlayerControllerSM { get => _playerControllerSM; }
    public PlayerControls Controls { get => _controls; }
    public PlayerMovements PlayerMovements { get => _playerMovements; }
    public PlayerInteractions PlayerInteractions { get => _playerInteractions; }
    public PlayerSwitchMatches PlayerSwitchMatches { get => _playerSwitchMatches; }
    public PlayerLaunchMatches PlayerLaunchMatches { get => _playerLaunchMatches; }
    public Insanity PlayerInsanity {  get => _playerInsanity; }
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
