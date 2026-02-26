using Unity.VisualScripting;
using UnityEngine;

public class Matches : MonoBehaviour
{
    private string nameSkin;
    public string possessed;

    private void Awake()
    {
        nameSkin = gameObject.name;

        if (possessed != "True") Load();
    }

    public void Save()
    {
        // -- Save skins -- //

        possessed = "True";

        PlayerPrefs.SetString(
            nameSkin,
            possessed.ToSafeString()
        );
    }

    public void Load()
    {
        // -- reclaims skins -- //

        possessed = PlayerPrefs.GetString(nameSkin);
    }
}
