using UnityEngine;

public class PlayerInteractState : PlayerActionState
{
    public PlayerInteractState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerActionStates states) : base(stateMachine, playerReferences, states)
    {
    }

    public override void Enter()
    {
        Debug.Log("[PLAYER - ACTION STATE] | ENTER INTERACT");

        _playerReferences.PlayerInteractions.TryInteract();

    }

    public override void Exit()
    {
        //Debug.Log("[PLAYER - ACTION STATE] | EXIT INTERACT");
    }

    public override void Update()
    {
        if (_playerReferences.Controls.WantToThrow)
        {
            _stateMachine.TransitionTo(_actionStates.Throw);
            return;
        }

        /* else */
        _stateMachine.TransitionTo(_actionStates.None);
        return;

    }
}
