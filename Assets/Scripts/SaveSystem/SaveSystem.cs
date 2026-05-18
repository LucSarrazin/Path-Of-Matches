using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    /* - Save JSON File Path Way - */
    private string _savePath;

    private void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log($"Save file path location :{_savePath}");
    }

    // --- Method : Save --- //
    public void SaveGame(float playerPosX, float playerPosY,float playerPosZ, int matchesCount, float pointerSensitivity)
    {
        /* Create a new SaveData Object and add new settings */
        SaveData data = new SaveData();
        data._playerPosX = playerPosX;
        data._playerPosY = playerPosY;
        data._playerPosZ = playerPosZ;
        data._matchesCount = matchesCount;
        data._pointerSensitivity = pointerSensitivity;

        /* Convert to JSON text */
        string json = JsonUtility.ToJson(data, true);

        /* Write in JSON file*/
        File.WriteAllText(_savePath, json);

        Debug.Log("Save complete");
    }

    // --- Method : Load --- //
        
    public SaveData LoadGame()
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
}
