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

        if (_playerReferences.PlayerLaunchMatches.AutoReleased)
        {
            // Une allumette s'est consumée pendant qu'on était en None
            // On consomme le flag et on laisse launchCanceled() sortir normalement l'allumette
            _playerReferences.PlayerLaunchMatches.ConsumeAutoRelease();
        }

        _playerReferences.PlayerLaunchMatches.StartThrowCharge();

    }
    public override void Exit()
    {
        if (_playerReferences.PlayerLaunchMatches.AutoReleased)
        {
            // L'allumette a déjà été lancée automatiquement, on ne rappelle pas StopThrowCharge
            _playerReferences.PlayerLaunchMatches.ConsumeAutoRelease();
            return;
        }

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
