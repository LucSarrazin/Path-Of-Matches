public abstract class PlayerState : IState
{
    protected StateMachine _stateMachine;
    protected PlayerReferences _playerReferences;
    protected PlayerStates _playerStates;

    public PlayerState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerStates playerStates)
    {
        _stateMachine = stateMachine;
        _playerReferences = playerReferences;
        _playerStates = playerStates;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
