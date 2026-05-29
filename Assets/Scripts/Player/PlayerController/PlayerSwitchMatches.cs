using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerSwitchMatches : MonoBehaviour
{
    [Header("SETTINGS : ")]
    public List<GameObject> listPrefabsMatches = new List<GameObject>();
    public List<GameObject> listSkinMatches = new List<GameObject>();
    public int skinNumber;

    [SerializeField] private PlayerReferences _playerReferences;
    public UnityEvent onSwitched;


    private void Start()
    {
    //     UpdateSkins();
    //
    //     Load();

        if (listSkinMatches.Count > 0)
        {
            _playerReferences.PlayerLaunchMatches.Matches = listSkinMatches[skinNumber];
        }
        else
        {
            _playerReferences.PlayerLaunchMatches.Matches = null;
        }
    }

    // public void UpdateSkins()
    // {
    //     // -- Updates the list of skins owned by the player at the scene launch -- //
    //
    //     foreach (GameObject skin in listPrefabsMatches)
    //     {
    //         if (skin.GetComponent<Matches>().possessed == "True")
    //         {
    //             listSkinMatches.Add(skin);
    //         }
    //     }
    // }

    public void Switch()
    {
        // -- Switch between matches skins -- //

        onSwitched?.Invoke(); // To Enable / Disable Watch view 
        
        if (listSkinMatches.Count == 0) return;

        if (skinNumber >= listSkinMatches.Count-1)
        {
            skinNumber = 0;
        }
        else
        {
            skinNumber++;
        }

        _playerReferences.PlayerLaunchMatches.Matches = listSkinMatches[skinNumber];
        
        // Save();
    }

    public void AddMatchesSkin(GameObject matches)
    {
        // -- Adds a new matches skin to the player's skin list -- //

        if (listPrefabsMatches.Contains(matches))
        {
            listSkinMatches.Add(matches);
            matches.GetComponent<Matches>().Save();
            _playerReferences.PlayerLaunchMatches.Matches = matches;
        }
    }

    // public void Save()
    // {
    //     // -- Save skinsID -- //
    //
    //     PlayerPrefs.SetString(
    //         "IDallumette",
    //         skinNumber.ToSafeString()
    //     );
    // }
    //
    // public void Load()
    // {
    //     // -- reclaims skinsID -- //
    //
    //     skinNumber = Convert.ToInt32(PlayerPrefs.GetString("IDallumette"));
    // }
}
