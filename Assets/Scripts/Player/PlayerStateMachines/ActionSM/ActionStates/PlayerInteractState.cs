using UnityEngine;

public class PlayerInteractState : PlayerActionState
{
    public PlayerInteractState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerActionStates states) : base(stateMachine, playerReferences, playerStates)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - ACTION STATE] | ENTER INTERACT");
    }

    public override void Exit()
    {
        Debug.Log("[PLAYER - ACTION STATE] | EXIT INTERACT");
    }

    public override void Update()
    {
        if (_playerReferences.Controls.WantToThrow)
        {
            _stateMachine.TransitionTo(_playerStates.Throw);
            return;
        }

        /* Change by "else" ? */
        if (!_playerReferences.Controls.WantToInteract && !_playerReferences.Controls.WantToThrow)
        {
            _stateMachine.TransitionTo(_playerStates.None);
            return;
        }

    }
}
