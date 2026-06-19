using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private PlayerReferences playerReferences;

    private void Start()
    {
        if (saveSystem == null) { FindAnyObjectByType(typeof(SaveSystem)); }

        saveSystem.LoadGame();
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

    IEnumerator Death()
    {
        playerReferences.PlayerAudioSource.PlayOneShot(playerReferences.deathSound);
        playerReferences.blinkingAnimation.SetBool("Death", true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationQuit()
    {
        GameEvents.OnDeleteSaveRequested?.Invoke();
    }
}

