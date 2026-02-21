public class StateMachine
{
    private IState _currentState; 

    public void Update()
    {
        _currentState?.Update();
    }

    public void TransitionTo(IState state)
    {
        /* [SAFETY] : check to avoid loop on same state */
        if (_currentState == state) return;

        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter(); 
    }
}
