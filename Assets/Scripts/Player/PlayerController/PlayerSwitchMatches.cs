using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerSwitchMatches : MonoBehaviour
{
    [Header("SETTINGS : ")]
    public List<GameObject> listPrefabsMatches = new List<GameObject>();

    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private List<GameObject> listSkinMatches = new List<GameObject>();
    private int skinNumber;


    private void Start()
    {
        UpdateSkins();
    }

    public void UpdateSkins()
    {
        foreach (GameObject skin in listPrefabsMatches)
        {
            if (skin.GetComponent<Matches>().possessed == "True")
            {
                listSkinMatches.Add(skin);
            }
        }
    }

    public void Switch()
    {
        if (skinNumber == listSkinMatches.Count)
        {
            skinNumber = 1;
        }
        else
        {
            skinNumber++;
        }

        _playerReferences.PlayerLaunchMatches.Matches = listSkinMatches[skinNumber-1];
    }

    public void AddMatchesSkin(GameObject matches)
    {
        if (listPrefabsMatches.Contains(matches))
        {
            listSkinMatches.Add(matches);
            matches.GetComponent<Matches>().Save();
        }
    }
}
