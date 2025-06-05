using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpNotifier", menuName = "Scriptable Objects/LevelUpNotifier")]
public class LevelUpNotifier : ScriptableObject
{
    public delegate void LevelUpUpdate(LevelData newLevel);
    public event LevelUpUpdate OnLevelUp;

    [NonSerialized]
    private float _accumulatedRequiredScore = 0.0f;
    public float RequiredScoreForNextLevelUp => _accumulatedRequiredScore;

    public void LevelUpTo(LevelData newLevel)
    {
        _accumulatedRequiredScore += newLevel.NextLevelRequiredScore;
        OnLevelUp?.Invoke(newLevel);
    }
}
