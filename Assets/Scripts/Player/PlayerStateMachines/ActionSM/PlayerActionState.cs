using UnityEngine;

public class PlayerActionStates
{
    protected StateMachine _stateMachine;
    protected PlayerReferences _playerReferences;
    protected PlayerActionStates _actionStates;

    public PlayerActionState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerActionStates actionStates)
    {
        _stateMachine = stateMachine;
        _playerReferences = playerReferences;
        _actionStates = actionStates;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

}
