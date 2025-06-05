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

    private void SetPlayerScore(float score)
    {
        _playerScore = Mathf.Clamp(score, 0, Mathf.Infinity);
        OnScoreUpdate?.Invoke(score);
        Debug.Log(_playerScore);
    }
}
