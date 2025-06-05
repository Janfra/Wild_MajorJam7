using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScore", menuName = "Scriptable Objects/PlayerScore")]
public class PlayerScore : ScriptableObject
{
    public delegate void ScoreUpdated(float score);
    public event ScoreUpdated OnScoreUpdate;

    [NonSerialized]
    private float _playerScore;
    public float Score { get => _playerScore; set => SetPlayerScore(value); }

    [SerializeField]
    private LevelData _StartingLevel;

    [SerializeField]
    private LevelUpNotifier _levelUpNotifier;

    [NonSerialized]
    private LevelData _currentLevel;

    public void RestartScoreAndLevel()
    {
        _currentLevel = _StartingLevel;
        _levelUpNotifier.LevelUpTo(_currentLevel);
    }

    private void SetPlayerScore(float score)
    {
        _playerScore = Mathf.Clamp(score, 0, Mathf.Infinity);
        OnScoreUpdate?.Invoke(score);
        if (_playerScore >= _currentLevel.NextLevelRequiredScore)
        {
            if (_currentLevel.NextLevel)
            {
                _levelUpNotifier.LevelUpTo(_currentLevel.NextLevel);
                _currentLevel = _currentLevel.NextLevel;
            }
        }
    }
}
