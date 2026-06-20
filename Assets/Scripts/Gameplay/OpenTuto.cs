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

        _playerReferences.PlayerControllerSM.ActionStateMachine.TransitionTo(_playerReferences.PlayerControllerSM.ActionStates.Interact);

        Quaternion newRotationQuaternion = Quaternion.Euler(0, 0, 150); /*Initial rotation of the page to be instantly readable*/

        _pageInstructions.transform.rotation = newRotationQuaternion; 
    }
}