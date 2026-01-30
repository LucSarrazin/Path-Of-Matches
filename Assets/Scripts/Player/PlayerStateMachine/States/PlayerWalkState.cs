using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerStates playerStates) : base(stateMachine, playerReferences, playerStates)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - STATE] | ENTER WALK STATE");
        _playerReferences.PlayerMovements.CanMove(true);
        _playerReferences.PlayerMovements.SetSpeed(_playerReferences.WalkSpeed);
    }

    public override void Exit()
    {
        //throw new System.NotImplementedException();
    }

    public override void Update()
    {
        _playerReferences.PlayerMovements.SetMoveInputs(_playerReferences.Controls.MoveInputs);

        /* TRANSITIONS */
        if (_playerReferences.Controls.MoveInputs.sqrMagnitude < 0.01f)
        {
            _stateMachine.TransitionTo(_playerStates.Idle);
            return;
        }

    }
}
