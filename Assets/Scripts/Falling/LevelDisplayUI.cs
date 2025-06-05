using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LevelDisplayUI : MonoBehaviour
{
    [SerializeField]
    private LevelUpNotifier _levelUpNotifier;

    [SerializeField]
    private TMP_Text _levelDisplay;

    private int levelCounter = 0;

    private void Awake()
    {
        if (!_levelDisplay)
        {
            _levelDisplay = GetComponent<TMP_Text>();
        }

        if (_levelUpNotifier)
        {
            _levelUpNotifier.OnLevelUp += UpdateLevelCounter;
        }
        else
        {
            _levelDisplay.text = "";
        }
    }

    private void UpdateLevelCounter(LevelData newLevel)
    {
        levelCounter++;
        _levelDisplay.text = $"LV. {levelCounter}";
    }
}
