public class PlayerStates
{
    private PlayerIdleState _idle;
    private PlayerWalkState _walk; 

    public PlayerIdleState Idle => _idle;
    public PlayerWalkState Walk => _walk;

    public PlayerStates(StateMachine stateMachine, PlayerReferences playerReferences)
    {
        _idle = new PlayerIdleState(stateMachine, playerReferences, playerStates: this);
        _walk = new PlayerWalkState(stateMachine, playerReferences, playerStates: this); 
    }
    
}
