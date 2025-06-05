using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerScoreUI : MonoBehaviour
{
    [SerializeField]
    private Slider _progressSlider;

    [SerializeField]
    private PlayerScore _playerScore;

    [SerializeField]
    private LevelUpRequirements _scoreRequirements;

    private void Awake()
    {
        if (_playerScore)
        {
            _playerScore.OnScoreUpdate += UpdateSlider;
        }

        if (!_progressSlider)
        {
            _progressSlider = GetComponent<Slider>();
        }
    }

    private void Start()
    {
        _progressSlider.value = 0;
    }

    private void UpdateSlider(float score)
    {
        _progressSlider.minValue = _scoreRequirements.MinScore;
        _progressSlider.maxValue = _scoreRequirements.MaxScore;
        _progressSlider.value = score;
    }
}
