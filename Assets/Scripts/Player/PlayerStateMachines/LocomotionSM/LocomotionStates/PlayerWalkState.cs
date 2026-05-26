using Unity.VisualScripting;
using UnityEngine;

public class PlayerWalkState : PlayerLocomotionState
{
    // * -- Variables --
    private float _footstepTimer;
    //private float _footstepInterval = 0.5f; 
    public PlayerWalkState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerLocomotionStates playerStates) : base(stateMachine, playerReferences, playerStates)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - STATE] | ENTER WALK STATE");
        _playerReferences.PlayerMovements.CanMove(true);
        _playerReferences.PlayerMovements.CanLook(true);

        _playerReferences.PlayerMovements.SetSpeed(_playerReferences.WalkSpeed);

        _footstepTimer = 0f;
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

        if (_playerReferences.Controls.WantToRun)
        {
            _stateMachine.TransitionTo(_playerStates.Run);
            return;
        }

    }

    private void PlayFootSteps()
    {
        //Debug.Log("Try play footsteps");
        _footstepTimer -= Time.deltaTime;

        if (_footstepTimer <= 0f)
        {
            _playerReferences.PlayerFootStep.PlayFootstep();

            _footstepTimer = _playerReferences.PlayerFootStep.FootStepInterval;
        }
    }
}
