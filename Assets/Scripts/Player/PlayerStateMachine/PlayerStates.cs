public class PlayerStates
{
    private PlayerIdleState _idle;
    private PlayerWalkState _walk;
    private PlayerRunState _run;
    private PlayerInteractState _interact;
    private PlayerThrowState _throw;

    public PlayerIdleState Idle => _idle;
    public PlayerWalkState Walk => _walk;
    public PlayerRunState Run => _run;
    public PlayerInteractState Interact => _interact;
    public PlayerThrowState Throw => _throw;

    public PlayerStates(StateMachine stateMachine, PlayerReferences playerReferences)
    {
        _idle = new PlayerIdleState(stateMachine, playerReferences, playerStates: this);
        _walk = new PlayerWalkState(stateMachine, playerReferences, playerStates: this);
        _run = new PlayerRunState(stateMachine, playerReferences, playerStates: this);
        _interact = new PlayerInteractState(stateMachine, playerReferences, playerStates: this);
        _throw = new PlayerThrowState(stateMachine, playerReferences, playerStates: this);
    }
    
}
