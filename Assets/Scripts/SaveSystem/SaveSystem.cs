using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    [Header("[REFERENCES]")]
    [SerializeField] private PlayerReferences _playerReferences;

    /* - Save JSON File Path Way - */
    private string _savePath;

    private bool _isNewSave = false; // JSON is or is not written

    //[SerializeField] private bool _isSceneEntrancePosition = false; // To avoid LookAt Target method if it is a save of player entrance position 

    private void OnEnable()
    {
        GameEvents.OnAutoSaveRequested += AutoSave;
        GameEvents.OnLoadRequested += LoadGame;
        GameEvents.OnDeleteSaveRequested += DeleteSave;
    }

    private void OnDisable()
    {
        GameEvents.OnAutoSaveRequested -= AutoSave;
        GameEvents.OnLoadRequested -= LoadGame;
        GameEvents.OnDeleteSaveRequested -= DeleteSave;
    }

    private void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log($"Save file path location :{_savePath}");
    }

    // * --- Methods : Save --- * //
    public void AutoSave(Transform target, bool isSceneEntrancePosition = false)
    {
        int matchesCount = _playerReferences.PlayerLaunchMatches.NumberOfMatches;
        float pointerSensitiviy = _playerReferences.PointerSensitivity;
        _isNewSave = false;
        SaveGame(target, matchesCount, pointerSensitiviy, isSceneEntrancePosition);
    }


    private void SaveGame(Transform target, int matchesCount, float pointerSensitivity, bool isSceneEntrancePosition)
    {
        /* Create a new SaveData Object and add new settings */
        SaveData data = new SaveData();

        // * -- Scene's Index -- * //
        data.SceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        // * -- If it is a save of player position -- * //
        data.IsSceneEntrancePosition = isSceneEntrancePosition;

        // * -- Target Position -- * /
        data._targetPosX = target.position.x /*+1*/ ; // try add only 1 meter to check
        data._targetPosY = target.position.y;
        data._targetPosZ = target.position.z;

        // * -- Variables * -- //
        data._matchesCount = matchesCount;
        data._pointerSensitivity = pointerSensitivity;

        /* Convert to JSON text */
        string json = JsonUtility.ToJson(data, true);

        /* Write in JSON file*/
        File.WriteAllText(_savePath, json);

        GameEvents.OnSaveComplete?.Invoke(); 

        //Debug.Log($"[SAVE COMPLETE] Scene index {data.SceneBuildIndex}");
        //Debug.Log($"Future player position X = {data._targetPosX} | Y = {data._targetPosY} | Z = {data._targetPosZ} ");
    }

    // * --- Method : Load --- * //
    public void LoadGame()
    {
        Debug.Log("LoadGame");
        SaveData data = LoadSave();
        int _currentScene = SceneManager.GetActiveScene().buildIndex;

        if (_currentScene != 0) // If not in Menu Scene - SAFETY 
        {
            if (_isNewSave || _currentScene != data.SceneBuildIndex) // IF JSON FILE DON'T EXIST OR THIS SCENE ISN'T THE SAME ONE 
            {
                AutoSave(_playerReferences.transform, true);
                Debug.Log("AutoSave initial player positon");
                return;

            }
            else if (data.SceneBuildIndex == _currentScene)
            {
                /* -- Get player initial position -- */
                Transform body = _playerReferences.Body;
                Transform head = _playerReferences.Head;
                //Debug.Log($"Initial body position :  X = {body.position.x} | Y = {body.position.y} | Z = {body.position.z} ");

                // * - Move Player to last save spawn point - * //
                Vector3 targetPos = new Vector3(data._targetPosX, data._targetPosY, data._targetPosZ);
                //Debug.Log($"Target position : X = {data._targetPosX} | Y = {data._targetPosY} | Z = {data._targetPosZ}");

                if (data.IsSceneEntrancePosition)
                {
                    Vector3 playerNewPos = new Vector3(data._targetPosX, data._targetPosY, data._targetPosZ);
                    body.position = playerNewPos;
                    return;
                }
                else // IF IT'S A FIRE CAMP !
                {
                    Vector3 playerNewPos = new Vector3(data._targetPosX + 1, data._targetPosY +1, data._targetPosZ);
                    body.position = playerNewPos;
                    // * - Look at target (firecamp save point) position - * //
                    head.LookAt(targetPos);

                    float rotY = head.eulerAngles.y;
                    body.rotation = Quaternion.Euler(0f, rotY, 0f);

                    float rotX = head.eulerAngles.x;
                    if (rotX > 180f) rotX -= 360f;
                    _playerReferences.PlayerMovements.SetXRotation(rotX);
                    head.localRotation = Quaternion.Euler(rotX, 0f, 0f);
                }
            }
        }
    }

    public SaveData LoadSave()
    {
        if (!File.Exists(_savePath))
        {
            Debug.LogWarning("Last save file not found -> create a new save");
            _isNewSave = true;
            return new SaveData(); // return empty object (default)
        }

        //Read file
        string json = File.ReadAllText(_savePath);

        //Convert to C# Object
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        //Debug.Log("Last save loaded ! ");
        return data;
    }


    // * --- Method : Delete --- * //

    public void DeleteSave()
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
