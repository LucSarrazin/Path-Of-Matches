using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public static PlayerReferences Instance { get; private set; }

    [Header("Player movements :")]
    [SerializeField] private float _walkSpeed;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerControls _controls;

    #region PUBLIC : 

    // VARIABLES //

    public float WalkSpeed { get => _walkSpeed; }

    // COMPONENTS //

    public Rigidbody Rigidbody { get => _rigidbody; }

    // SCRIPTS //

    public PlayerControls Controls { get => _controls; }

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
