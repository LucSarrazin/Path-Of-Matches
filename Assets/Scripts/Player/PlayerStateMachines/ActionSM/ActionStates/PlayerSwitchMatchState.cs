using UnityEngine;

public class PlayerSwitchMatchState : PlayerActionState
{
    public PlayerSwitchMatchState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerActionStates states) : base(stateMachine, playerReferences, states)
    {
    }
    public override void Enter()
    {
        Debug.Log("[PLAYER - ACTION STATE] | ENTER SWITCH MATCH");
        ResetActionComplete();

        _playerReferences.PlayerMovements.CanMove(false);

        // Changes the player's matches skin //
        _playerReferences.PlayerSwitchMatches.Switch();

        CompleteAction();
    }

    public override void Exit()
    {
        _playerReferences.PlayerMovements.CanMove(true);
    }

    public override void Update()
    {
        if (_playerReferences.Controls.WantToThrow)
        {
            _stateMachine.TransitionTo(_actionStates.Throw);
            return;
        }
        if (_playerReferences.Controls.WantToInteract)
        {
            _stateMachine.TransitionTo(_actionStates.Interact);
            return;
        }

        /* Transition to None State when action is complete */
        CompleteAction(); 
    }
}
