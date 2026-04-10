using UnityEngine;

[CreateAssetMenu(fileName = "InspectableDataSO", menuName = "Scriptable Objects/InspectableObjectData")]
public class InspectableObjectData : ScriptableObject
{
    [Header("[DATAS]")]
    [SerializeField] private string _name; 
    [SerializeField] private string _description;

    public string Name => _name;
    public string Description => _description;
}
