using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState (StateMachine stateMachine, PlayerReferences playerReferences, PlayerLocomotionStates playerStates) : base(stateMachine, playerReferences, playerStates)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - STATE] | ENTER RUN STATE");
        _playerReferences.PlayerMovements.CanMove(true);
        _playerReferences.PlayerMovements.SetSpeed(_playerReferences.RunSpeed);
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
        /* WALK TRANSITION */
        if (!_playerReferences.Controls.WantToRun)
        {
            _stateMachine.TransitionTo(_playerStates.Walk);
            return;
        }

        //if (_playerReferences.Controls.WantToInteract)
        //{
        //    _stateMachine.TransitionTo(_playerStates.Interact);
        //    return;
        //}

        //if (_playerReferences.Controls.WantToThrow)
        //{
        //    _stateMachine.TransitionTo(_playerStates.Throw);
        //    return;
        //}
    }
    
}
