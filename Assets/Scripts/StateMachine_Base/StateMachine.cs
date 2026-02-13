public class StateMachine
{
    private IState _currentState; 

    public void Update()
    {
        _currentState?.Update();
    }

    public void TransitionTo(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter(); 
    }
}
