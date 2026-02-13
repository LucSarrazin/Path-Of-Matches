using UnityEngine;

public class PlayerNoneStatePlayerActionState
{

    public PlayerThrowState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerActionStates states) : base(stateMachine, playerReferences, playerStates)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - STATE ACTION] | ENTER NONE");
    }

    public override void Exit()
    {
        Debug.Log("[PLAYER - STATE ACTION] | EXIT NONE");
    }

    public override void Update()
    {
        if (_playerReferences.Controls.WantToThrow)
        {
            _stateMachine.TransitionTo(_playerStates.Throw);
            return;
        }

        if (_playerReferences.Controls.WantToInteract)
        {
            _stateMachine.TransitionTo(_playerStates.Interact);
            return;
        }
    }
}
