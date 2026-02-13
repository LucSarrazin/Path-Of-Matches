public class PlayerActionStates
{
    private PlayerNoneState _none;
    private PlayerInteractState _interact;
    private PlayerThrowState _throw;
    private PlayerSwitchMatchState _switchMatch;

    public PlayerNoneState None => _none;
    public PlayerInteractState Interact => _interact;
    public PlayerThrowState Throw => _throw;
    public PlayerSwitchMatchState SwitchMatch => _switchMatch;

    public PlayerActionStates(StateMachine stateMachine, PlayerReferences references)
    {
        _none = new PlayerNoneState(stateMachine, references, this);
        _interact = new PlayerInteractState(stateMachine, references, this);
        _throw = new PlayerThrowState(stateMachine, references, this);
        _switchMatch = new PlayerSwitchMatchState(stateMachine, references, this);

    }
}
