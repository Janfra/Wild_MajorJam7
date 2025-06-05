using System;
using UnityEngine;

public class CatchScorer : MonoBehaviour
{
    [SerializeField]
    private Catcher _catcher;

    [SerializeField]
    private PlayerScore _playerScore;

    [SerializeField]
    private GameEvent _fruitMissed;

    private void Awake()
    {
        if (!_catcher)
        {
            _catcher = GetComponent<Catcher>();
        }

        if (_catcher)
        {
            _catcher.OnSuccessfulCatch += AddCatchScore;
            _catcher.OnMissCatch += RemoveCatchScore;
        }

        if (_fruitMissed)
        {
            _fruitMissed.OnEvent += RemoveCatchScore;
        }

        if (!_playerScore)
        {
            throw new NullReferenceException($"Player scorer has no player score to set the score to. Please assign one {name}");
        }
    }

    private void AddCatchScore()
    {
        _playerScore.Score++;
    }

    private void RemoveCatchScore()
    {
        _playerScore.Score--;
    }
}
