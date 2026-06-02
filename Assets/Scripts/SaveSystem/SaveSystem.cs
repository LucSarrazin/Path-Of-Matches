using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    [Header("[REFERENCES]")]
    [SerializeField] private PlayerReferences _playerReferences;


    /* - Save JSON File Path Way - */
    private string _savePath;

    private void OnEnable()
    {
        GameEvents.OnAutoSaveRequested += AutoSave;
        GameEvents.OnLoadRequested += LoadGame;
    }

    private void OnDisable()
    {
        GameEvents.OnAutoSaveRequested -= AutoSave;
        GameEvents.OnLoadRequested -= LoadGame;
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

        // * -- Scene's Index -- * //
        data.SceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        
        // * -- Target Position -- * /
        data._targetPosX = target.position.x /*+1*/ ; // try add only 1 meter to check
        data._targetPosY = target.position.y ;
        data._targetPosZ = target.position.z;

        // * -- Variables * -- //
        data._matchesCount = matchesCount;
        data._pointerSensitivity = pointerSensitivity;

        /* Convert to JSON text */
        string json = JsonUtility.ToJson(data, true);

        /* Write in JSON file*/
        File.WriteAllText(_savePath, json);

        Debug.Log("Save complete");
        Debug.Log($"Future player position X = {data._targetPosX} | Y = {data._targetPosY} | Z = {data._targetPosZ} ");
    }

    // * --- Method : Load --- * //
    public void LoadGame()
    {
        SaveData data = LoadSave();

        if (data == null)
        {
            Debug.Log("First Game, no save to load");
            return;
        }

        /* -- Scene's index -- */
        if (SceneManager.GetActiveScene().buildIndex != data.SceneBuildIndex)
        {
            SceneManager.LoadScene(data.SceneBuildIndex);
        }
        /* -- Player Position -- */

        Transform body = _playerReferences.Body;
        Transform head = _playerReferences.Head;

        Vector3 targetPos = new Vector3(data._targetPosX, data._targetPosY, data._targetPosZ);

        Vector3 playerNewPos = new Vector3(data._targetPosX + 1, data._targetPosY, data._targetPosZ);
        body.position = playerNewPos;

        head.LookAt(targetPos);

        float rotY = head.eulerAngles.y;
        body.rotation = Quaternion.Euler(0f, rotY, 0f);

        float rotX = head.eulerAngles.x;
        if (rotX > 180f) rotX -= 360f;
        //_xRotation = rotX;
        _playerReferences.PlayerMovements.SetXRotation(rotX);
        head.localRotation = Quaternion.Euler(rotX, 0f, 0f);

        // * -- Variables * -- //
        _playerReferences.PlayerLaunchMatches.NumberOfMatches = data._matchesCount;
        _playerReferences.PointerSensitivity = data._pointerSensitivity;

    }

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
