using UnityEngine;

public class PlayerControllerSM : MonoBehaviour
{
    private StateMachine _locomotionStateMachine;
    private StateMachine _actionStateMachine;
    //private StateMachine _stateMachine;
    //private PlayerStates _states;
    private PlayerStates _locomotionStates;
    private PlayerStates _actionStates;
    private PlayerReferences _references;

    private void Awake()
    {
        _references = GetComponentInParent<PlayerReferences>(); 
        if (_references == null) { Debug.Log("Player refs aren't charged");  }

        /* --- Initialisation des States Machines : --- */
        //_stateMachine = new StateMachine();

        _locomotionStateMachine = new StateMachine();
        _actionStateMachine = new StateMachine();
        //_stateMachine = new StateMachine();

        _locomotionStates = new PlayerStates(_locomotionStateMachine, _references);
        _actionStateMachine = new PlayerActionStates(_actionStateMachine, _references);
        //

        _locomotionStateMachine.TransitionTo(_locomotionStates.Idle);
        _actionStateMachine.TransitionTo(_actionStates.None);
    }

    private void Update()
    {
        _locomotionStateMachine.Update();
        _actionStateMachine.Update();
    }
}
