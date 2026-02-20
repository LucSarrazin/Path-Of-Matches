using UnityEngine;

public class PlayerInteractState : PlayerActionState
{
    private IInteractable _interactable;
    private Inspectable _inspectable;
    private bool _isInspecting;

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

        /* -- If get inspectable interactable : freeze movements and rotations -- */
        _inspectable = _interactable as Inspectable;

        if (_inspectable != null)
        {
            _playerReferences.PlayerMovements.CanMove(!_inspectable.FreezeMovement ? true : false);
            _playerReferences.PlayerMovements.CanLook(!_inspectable.FreezeRotationLook ? true : false);


            /* -- If inspectable : long interaction -- */
            _playerReferences.PlayerInteractions.TryInteract();

            _isInspecting = true;
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
        //Debug.Log("[PLAYER - ACTION STATE] | EXIT INTERACT");

        _playerReferences.PlayerMovements.CanMove(true);
        _playerReferences.PlayerMovements.CanLook(true);

        _inspectable = null;
        _interactable = null;
    }

    public override void Update()
    {
        if (_isInspecting)
        {
            /* If input is pressed again : stop the inspection */
            if (_playerReferences.Controls.WantToInteract)
            {
                StopInspection(); 
            }
        }

        if (_playerReferences.Controls.WantToThrow)
        {
            _stateMachine.TransitionTo(_actionStates.Throw);
            return;
        }

        /* Check if action is over/complete */
        TryCompleteAction();

    }

    /* -- Special method for inspectable types */

    private void StopInspection()
    {
        /* Add event to close UI panel */
        CompleteAction();
    }
}
