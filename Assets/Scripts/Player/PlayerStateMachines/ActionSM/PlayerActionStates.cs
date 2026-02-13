public class PlayerActionStates
{
    private PlayerNoneState _none;
    private PlayerInteractState _interact;
    private PlayerThrowState _throw;

    public PlayerNoneState None => _none;
    public PlayerInteractState Interact => _interact;
    public PlayerThrowState Throw => _throw;

    public PlayerActionStates(StateMachine stateMachine, PlayerReferences references)
    {
        _none = new PlayerNoneState(stateMachine, references, this);
        _interact = new PlayerInteractState(stateMachine, references, this);
        _throw = new PlayerThrowState(stateMachine, references, this);

    }
}
