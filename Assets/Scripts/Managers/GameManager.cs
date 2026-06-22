using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private PlayerReferences _playerReferences;

    private void Start()
    {
        if (_saveSystem == null)
            _saveSystem = FindAnyObjectByType<SaveSystem>();

        if (_playerReferences == null)
            _playerReferences = FindAnyObjectByType<PlayerReferences>();

        if (_saveSystem != null)
            _saveSystem.LoadGame();

        if (StaticDatasFlags.SceneLoadedAfterDeath)
        {
            if (_playerReferences != null)
            {
                _playerReferences.PlayerAudioSource.PlayOneShot(_playerReferences.reviveSound);
                _playerReferences.blinkingAnimation.SetBool("Start", true);
                //Debug.Log("Try play revive sound");
            }

            StaticDatasFlags.SceneLoadedAfterDeath = false;
        }
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerDeath += PlayerDeath;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDeath -= PlayerDeath;
    }

    private void PlayerDeath()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        Debug.Log("Start Death Routine");

        if (_playerReferences != null)
        {
            _playerReferences.PlayerAudioSource.PlayOneShot(_playerReferences.deathSound);
            _playerReferences.blinkingAnimation.SetBool("Death", true);
        }

        StaticDatasFlags.SceneLoadedAfterDeath = true;
        Debug.Log($"SceneLoadedAfterDeath = {StaticDatasFlags.SceneLoadedAfterDeath}");

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationQuit()
    {
        GameEvents.OnDeleteSaveRequested?.Invoke();
    }
}