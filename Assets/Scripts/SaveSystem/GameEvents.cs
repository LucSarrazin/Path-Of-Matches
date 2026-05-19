using System;
using UnityEngine;

/// <summary>
/// Script to manage general game events on the project, it's a Event Bus Pattern
/// </summary>
public static class GameEvents
{
    public static Action<Transform> OnAutoSaveRequested;
    public static Action OnLoadRequested;

    //Maybe later : 
    //public static Action OnDieCharacter;
}