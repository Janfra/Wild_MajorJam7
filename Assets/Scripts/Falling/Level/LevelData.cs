using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private LevelData _nextLevel;
    public LevelData NextLevel => _nextLevel;

    [SerializeField]
    private LevelSpawnData _spawnData;
    public LevelSpawnData SpawnData => _spawnData;

    [SerializeField]
    private float _nextLevelScore = 5;
    public float NextLevelRequiredScore => _nextLevelScore; 
}
