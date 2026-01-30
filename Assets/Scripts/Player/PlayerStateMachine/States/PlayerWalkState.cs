using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerStates playerStates) : base(stateMachine, playerReferences, playerStates)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - STATE] | ENTER WALK STATE");
        _playerReferences.PlayerMovements.MovePlayer(/*_playerReferences.WalkSpeed*/);
    }

    public override void Exit()
    {
        //throw new System.NotImplementedException();
    }

    public override void Update()
    {
        _playerReferences.PlayerMovements.MovePlayer(/*_playerReferences.WalkSpeed*/);

        if (Mathf.Approximately(_playerReferences.Controls.MoveInputs.x, 0f))
        {
            _stateMachine.TransitionTo(_playerStates.Idle);
            return;
        }

    }
}
