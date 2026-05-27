using UnityEngine;

public class PlayerRunState : PlayerLocomotionState
{
    private float _footstepTimer;

    public PlayerRunState (StateMachine stateMachine, PlayerReferences playerReferences, PlayerLocomotionStates playerStates) : base(stateMachine, playerReferences, playerStates)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - STATE] | ENTER RUN STATE");
        _playerReferences.PlayerMovements.CanMove(true);
        _playerReferences.PlayerMovements.CanLook(true);
        _playerReferences.PlayerMovements.SetSpeed(_playerReferences.RunSpeed);
    }

    public override void Exit()
    {
        //throw new System.NotImplementedException();
    }

    public override void Update()
    {
        _playerReferences.PlayerMovements.SetMoveInputs(_playerReferences.Controls.MoveInputs);

        PlayFootSteps();

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
    }

    private void PlayFootSteps()
    {
        _footstepTimer -= Time.deltaTime;

        if (_footstepTimer <= 0f)
        {
            _playerReferences.PlayerFootStep.PlayFootstep();

            float speed = _playerReferences.PlayerMovements.CurrentSpeed;

            _footstepTimer = 3f / speed; 
        }
    }

}
