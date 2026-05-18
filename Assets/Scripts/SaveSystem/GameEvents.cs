using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Script to manage general game events on the project
/// </summary>
public static class GameEvents
{
    public static Action<Transform> OnAutoSaveRequested;

    //Maybe later : 
    //public static Action OnLoadRequested; 
    //public static Action OnDieCharacter;
}