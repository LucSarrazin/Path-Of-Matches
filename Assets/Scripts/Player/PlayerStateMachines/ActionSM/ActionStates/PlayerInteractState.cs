using UnityEngine;

public class PlayerInteractState : PlayerActionState
{
    private IInteractable _interactable;
    private Inspectable _inspectable;
    private bool _isInspecting;
    private bool _needsToStartInteraction = false;

    public PlayerInteractState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerActionStates states) : base(stateMachine, playerReferences, states)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - ACTION STATE] | ENTER INTERACT");
        ResetActionComplete();

        /* -- Get object to interact with -- */
        _interactable = _playerReferences.PlayerInteractions.CurrentInteractable;
        if (_interactable == null) { CompleteAction(); return; } /* Stop action and go back to None State */

        ResetActionComplete();

        /* -- If get inspectable interactable : freeze movements and rotations -- */
        _inspectable = _interactable as Inspectable;

        if (_inspectable != null)
        {
            _playerReferences.Controls.IsInspecting = true;
            _playerReferences.Controls.MoveInputs = Vector2.zero;
            _playerReferences.Controls.CanThrow = false;
            _playerReferences.Controls.CanEscape = false;

            /* -- Cursor -- */
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _isInspecting = true;

            _needsToStartInteraction = true;

            ///* -- If inspectable : long interaction -- */
            //_isInspecting = true;

            /* -- Reset Hand Animation -- */
            //Un peu schlag ---- A revoir !
            _playerReferences.Watch.SetBoolSwitch(false); 
            _playerReferences.Watch.openWatch();
            //_playerReferences.Watch.WatchAnimator.SetBool("Close", true);

        }
        else
        {
            /* -- If NOT inspectable : instant interaction -- */
            _playerReferences.PlayerInteractions.TryInteract();
            CompleteAction();

        }
    }

    public override void Exit()
    {
        //Debug.Log("[INTERACT STATE] Exit() appelé");
        _playerReferences.Controls.IsInspecting = false;
        _isInspecting = false; // reset explicite
        _playerReferences.Controls.CanThrow = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _playerReferences.Controls.CanEscape = true;

        _inspectable = null;
        _interactable = null;
    }

    public override void Update()
    {
        //Debug.Log($"[INTERACT STATE] Update | _isInspecting={_isInspecting} | _inspectable={_inspectable}");

        if (_needsToStartInteraction)
        {
            //Debug.Log("[INTERACT STATE] Appel Open()");

            _needsToStartInteraction = false;
            _inspectable.Open(); //  à la place de TryInteract()
        }

        if (_isInspecting && _inspectable != null)
        {
            //Debug.Log($"[INTERACT STATE] IsDraggingInspectable={_playerReferences.Controls.IsDraggingInspectable}");

            _inspectable.SetDragging(_playerReferences.Controls.IsDraggingInspectable);

            if (_playerReferences.Controls.WantToInteract)
                StopInspection();
            return;
        }

        TryCompleteAction();
    }

    /* -- Special method for inspectable types */

    private void StopInspection()
    {
        _inspectable.Close(); // Close() uniquement ici
        _isInspecting = false;
        CompleteAction();
    }
}
