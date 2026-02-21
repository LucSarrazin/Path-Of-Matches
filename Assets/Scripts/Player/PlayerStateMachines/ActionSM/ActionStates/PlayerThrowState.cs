using UnityEngine;

public class PlayerThrowState : PlayerActionState
{
    public PlayerThrowState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerActionStates states) : base(stateMachine, playerReferences, states)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - STATE] | ENTER THROW");
        ResetActionComplete();

        _playerReferences.PlayerLaunchMatches.StartThrowCharge();

    }

    public override void Exit()
    {
        //Debug.Log("[PLAYER - STATE ACTION] | EXIT THROW");
        _playerReferences.PlayerLaunchMatches.StopThrowCharge(); 
    }

    public override void Update()
    {
        if (!_playerReferences.Controls.WantToThrow)
        {
            /* Transition to None State when action is complete */
            CompleteAction();
        }

        if (_playerReferences.Controls.WantToInteract)
        {
            _stateMachine.TransitionTo(_actionStates.Interact);
            return;
        }

        if (_playerReferences.Controls.WantToSwitchMatch)
        {
            _stateMachine.TransitionTo(_actionStates.SwitchMatch);
            return;
        }

        TryCompleteAction();
    }


}
