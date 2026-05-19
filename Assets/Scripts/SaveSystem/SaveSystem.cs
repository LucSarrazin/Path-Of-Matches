using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("[REFERENCES]")]
    [SerializeField] private PlayerReferences _playerReferences;


    /* - Save JSON File Path Way - */
    private string _savePath;

    private void OnEnable()
    {
        GameEvents.OnAutoSaveRequested += AutoSave;
    }

    private void OnDisable()
    {
        GameEvents.OnAutoSaveRequested -= AutoSave;
    }

    private void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log($"Save file path location :{_savePath}");
    }


    // * --- Methods : Save --- * //
    public void AutoSave(Transform target)
    {
        int matchesCount = _playerReferences.PlayerLaunchMatches.NumberOfMatches;
        float pointerSensitiviy = _playerReferences.PointerSensitivity;
        SaveGame(target, matchesCount, pointerSensitiviy);
    }


    private void SaveGame(Transform target, int matchesCount, float pointerSensitivity)
    {
        /* Create a new SaveData Object and add new settings */
        SaveData data = new SaveData();
        data._playerPosX = target.position.x +1 ; // try add only 1 meter to check
        data._playerPosY = target.position.y ;
        data._playerPosZ = target.position.z;
        data._matchesCount = matchesCount;
        data._pointerSensitivity = pointerSensitivity;

        /* Convert to JSON text */
        string json = JsonUtility.ToJson(data, true);

        /* Write in JSON file*/
        File.WriteAllText(_savePath, json);

        Debug.Log("Save complete");
        Debug.Log($"Future player position X = {data._playerPosX} | Y = {data._playerPosY} | Z = {data._playerPosZ} ");
    }

    // * --- Method : Load --- * //

    public SaveData LoadSave()
    {
        if(!File.Exists(_savePath))
        {
            Debug.LogWarning("Last save file not found -> create a new save");
            return new SaveData(); // return empty object (default)
        }

        //Read file
        string json = File.ReadAllText(_savePath);

        //Convert to C# Object
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        Debug.Log("Last save loaded ! ");
        return data;
    }

    public void LoadGame(Transform player/*, int matchesCounter*/)
    {
        SaveData data = LoadSave();
        player.position = new Vector3(data._playerPosX, data._playerPosY, data._playerPosZ);

    }

    // * --- Method : Delete --- * //

    private void DeleteSave()
    {
        if (File.Exists(_savePath))
        {
            File.Delete(_savePath);
            Debug.Log("Last save Deleted ! ");
        }
        else
        {
            Debug.LogWarning("No saved file to delete !");
        }
    }
}
