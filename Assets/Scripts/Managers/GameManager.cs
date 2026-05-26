using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
}
