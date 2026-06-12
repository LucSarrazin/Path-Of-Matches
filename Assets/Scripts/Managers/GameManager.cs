using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationQuit()
    {
        //saveSystem.DeleteSave(); 
    }
}

