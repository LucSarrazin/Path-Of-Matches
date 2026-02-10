using UnityEngine;

public class PlayerThrowState : PlayerState
{

    public PlayerThrowState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerStates playerStates) : base(stateMachine, playerReferences, playerStates)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - STATE] | ENTER THROW");
    }

    public override void Exit()
    {
        //throw new System.NotImplementedException();
    }

    public override void Update()
    {
        if (_playerReferences.Controls.MoveInputs.sqrMagnitude < 0.01f)
        {
            _stateMachine.TransitionTo(_playerStates.Idle);
            return;
        }

        if (_playerReferences.Controls.MoveInputs != Vector2.zero)
        {
            _stateMachine.TransitionTo(_playerStates.Walk);
            return;
        }
    }

}
