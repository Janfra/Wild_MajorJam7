using System;
using TMPro;
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

    [SerializeField]
    private TMP_Text _scoreText;

    private void Awake()
    {
        if (_playerScore)
        {
            _playerScore.OnScoreUpdate += UpdateScoreDisplay;
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
        _progressSlider.maxValue = _levelUpNotifier.RequiredScoreForNextLevelUp;
        _progressSlider.value = _progressSlider.value;
    }

    private void Start()
    {
        _progressSlider.value = 0;
    }

    private void UpdateScoreDisplay(float score)
    {
        _progressSlider.value = score;
        if (_scoreText)
        {
            _scoreText.text = score.ToString();
        }
    }
}
