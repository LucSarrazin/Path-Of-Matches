public class PlayerLocomotionStates
{
    private PlayerIdleState _idle;
    private PlayerWalkState _walk;
    private PlayerRunState _run;

    public PlayerIdleState Idle => _idle;
    public PlayerWalkState Walk => _walk;
    public PlayerRunState Run => _run;

    public PlayerLocomotionStates(StateMachine stateMachine, PlayerReferences playerReferences)
    {
        _idle = new PlayerIdleState(stateMachine, playerReferences, this);
        _walk = new PlayerWalkState(stateMachine, playerReferences, this);
        _run = new PlayerRunState(stateMachine, playerReferences, this);
    }
    
}
