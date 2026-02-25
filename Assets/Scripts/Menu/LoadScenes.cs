using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void OnButtonClick(string scene)
    {
       SceneManager.LoadScene(scene);
    }
}
