using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpNotifier", menuName = "Scriptable Objects/LevelUpNotifier")]
public class LevelUpNotifier : ScriptableObject
{
    public delegate void LevelUpUpdate(LevelData newLevel);
    public event LevelUpUpdate OnLevelUp;

    public void LevelUpTo(LevelData newLevel)
    {
        OnLevelUp?.Invoke(newLevel);
    }
}
