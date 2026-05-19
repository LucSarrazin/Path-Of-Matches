using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("[REFERENCES]")]
    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private SaveSystem _saveSystem;
    void Start()
    {
        _saveSystem.LoadGame(_playerReferences.Body);

    }

    void Update()
    {
        
    }
}
