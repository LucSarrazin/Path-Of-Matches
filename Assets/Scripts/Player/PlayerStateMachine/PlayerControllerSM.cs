using UnityEngine;

public class PlayerControllerSM : MonoBehaviour
{
    private StateMachine _stateMachine;
    private PlayerStates _states;
    private PlayerReferences _references;

    private void Awake()
    {
        _references = GetComponentInParent<PlayerReferences>(); 
        if (_references == null) { Debug.Log("Player refs aren't charged");  } 
        _stateMachine = new StateMachine(); 
        _states = new PlayerStates(_stateMachine, _references);
        _stateMachine.TransitionTo(_states.Idle);
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}
