using UnityEngine;

public class PlayerThrowState : PlayerActionState
{

    public PlayerThrowState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerActionStates states) : base(stateMachine, playerReferences, playerStates)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - STATE] | ENTER THROW");
    }

    public override void Exit()
    {
        Debug.Log("[PLAYER - STATE ACTION] | EXIT THROW");
    }

    public override void Update()
    {

        if (_playerReferences.Controls.WantToThrow)
        {
            _stateMachine.TransitionTo(_playerStates.Throw);
            return;
        }

        /* Change by "else" ? */
        if (!_playerReferences.Controls.WantToInteract && !_playerReferences.Controls.WantToThrow)
        {
            _stateMachine.TransitionTo(_playerStates.None);
            return;
        }


    }

}
