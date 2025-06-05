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
    private LevelUpNotifier _levelUpNotifier;

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

        if (_levelUpNotifier)
        {
            _levelUpNotifier.OnLevelUp += UpdateUI;
        }
    }

    private void UpdateUI(LevelData newLevel)
    {
        _progressSlider.minValue = 0.0f;
        _progressSlider.maxValue = newLevel.NextLevelRequiredScore;
        _progressSlider.value = _progressSlider.value;
    }

    private void Start()
    {
        _progressSlider.value = 0;
    }

    private void UpdateSlider(float score)
    {
        _progressSlider.value = score;
    }
}
