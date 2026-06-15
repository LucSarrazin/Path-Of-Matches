using UnityEngine;

[CreateAssetMenu(fileName = "ScreamerDataSO", menuName = "Scriptable Objects/ScreamerData")]
public class ScreamerData : ScriptableObject
{
    [Header("[DATAS]")]
    public string screamerName;
    public GameObject screamerPrefab;
    public float destroyAfterSeconds;
    public string spawnpointName;
    [Header("[DESCRIPTION]")]
    public string description;
}
