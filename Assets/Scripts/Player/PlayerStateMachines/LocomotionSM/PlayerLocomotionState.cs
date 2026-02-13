public abstract class PlayerLocomotionState : IState
{
    protected StateMachine _stateMachine;
    protected PlayerReferences _playerReferences;
    protected PlayerLocomotionStates _playerStates;

    public PlayerLocomotionState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerLocomotionStates playerStates)
    {
        _stateMachine = stateMachine;
        _playerReferences = playerReferences;
        _playerStates = playerStates;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
