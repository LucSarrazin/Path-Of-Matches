using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [Header("Player movements :")]
    [SerializeField] private float _walkSpeed;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerControls _controls;
    [SerializeField] private PlayerMovements _playerMovements;

    #region PUBLIC REFERENCES : 

    // VARIABLES //

    public float WalkSpeed { get => _walkSpeed; }

    // COMPONENTS //

    public Rigidbody Rigidbody { get => _rigidbody; }

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
