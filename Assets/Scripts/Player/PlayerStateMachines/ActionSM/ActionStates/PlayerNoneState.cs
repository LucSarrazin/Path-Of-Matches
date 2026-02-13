using UnityEngine;

public class PlayerNoneState : PlayerActionState
{
    public PlayerNoneState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerActionStates states) : base(stateMachine, playerReferences, states)
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
            _stateMachine.TransitionTo(_actionStates.Throw);
            return;
        }

        if (_playerReferences.Controls.WantToInteract)
        {
            _stateMachine.TransitionTo(_actionStates.Interact);
            return;
        }
    }
}
