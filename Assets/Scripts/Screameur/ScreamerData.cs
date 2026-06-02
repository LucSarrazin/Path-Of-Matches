using UnityEngine;

[CreateAssetMenu(fileName = "ScreamerDataSO", menuName = "Scriptable Objects/ScreamerData")]
public class ScreamerData : ScriptableObject
{
    [Header("[DATAS]")]
    public string screamerName;
    public GameObject screamerPrefab;
    [Header("[DESCRIPTION]")]
    public string description;
}
