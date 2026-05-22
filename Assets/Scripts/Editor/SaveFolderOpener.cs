using UnityEditor;
using UnityEngine; 

public static class SaveFolderOpener
{
    [MenuItem("Tools/Open Persistent Data Folder")]
    public static void OpenFolder()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}