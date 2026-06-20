using System.Collections;
using UnityEngine;

public class OpenTuto : MonoBehaviour
{
    [SerializeField] private Inspectable _pageInstructions;
    [SerializeField] private PlayerReferences _playerReferences;

    private void Start()
    {
        StartCoroutine(OpenAfterSceneInit());
    }

    private IEnumerator OpenAfterSceneInit()
    {
        yield return null;

        _playerReferences.PlayerInteractions.CurrentInteractable = _pageInstructions;

        // À remplacer par ton vrai accès à la state machine d'action
        _playerReferences.PlayerControllerSM.ActionStateMachine.TransitionTo(
            _playerReferences.PlayerControllerSM.ActionStates.Interact);

        //Vector3 newRotation = new Vector3(0, 0, 90);
        Quaternion newRotationQuaternion = Quaternion.Euler(0, 0, 150);

        _pageInstructions.transform.rotation = newRotationQuaternion; 
    }
}