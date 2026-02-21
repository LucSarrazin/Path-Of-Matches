public abstract class PlayerActionState : IState
{
    protected StateMachine _stateMachine;
    protected PlayerReferences _playerReferences;
    protected PlayerActionStates _actionStates;

    private bool _actionIsComplete;

    public PlayerActionState(StateMachine stateMachine, PlayerReferences playerReferences, PlayerActionStates actionStates)
    {
        _stateMachine = stateMachine;
        _playerReferences = playerReferences;
        _actionStates = actionStates;
        _actionIsComplete = false;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

    /* Transition to None State when action is complete to avoid multiple calls to None State */
    protected void CompleteAction()
    {
        _actionIsComplete = true;
    }

    protected void TryCompleteAction()
    {
        if (_actionIsComplete)
        {
            _stateMachine.TransitionTo(_actionStates.None);
        }
    }

    protected void ResetActionComplete()
    {
        _actionIsComplete = false;
    }

}
