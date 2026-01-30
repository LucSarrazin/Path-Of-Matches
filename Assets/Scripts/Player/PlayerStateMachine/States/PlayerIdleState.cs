using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerStates playerStates) : base(stateMachine, playerReferences, playerStates)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - STATE] | ENTER IDLE");
    }

    public override void Exit()
    {
        //throw new System.NotImplementedException();
    }

    public override void Update()
    {
        if (_playerReferences.Controls.MoveInputs != Vector2.zero)
        {
            _stateMachine.TransitionTo(_playerStates.Walk);
            return;
        }
    }
}
