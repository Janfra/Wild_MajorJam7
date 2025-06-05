using UnityEngine;

[CreateAssetMenu(fileName = "LevelSpawnData", menuName = "Scriptable Objects/LevelSpawnData")]
public class LevelSpawnData : ScriptableObject
{
    [SerializeField]
    private float _spawnDelay = 3.0f;
    public float SpawnDelay => _spawnDelay;

    [SerializeField]
    private float _minFallSpeed = 140.0f;
    public float MinFallSpeed => _minFallSpeed;

    [SerializeField]
    private float _maxFallSpeed = 140.0f;
    public float MaxFallSpeed => _maxFallSpeed;

    [SerializeField]
    private float _minSpeedAtDistance = 10;
    public float MinSpeedAtDistance => _minSpeedAtDistance;

    [SerializeField]
    private AnimationCurve _speedReductionCurve; 
    public AnimationCurve SpeedReductionCurve => _speedReductionCurve;

    [SerializeField]
    private CatchActionAssociatedData[] _possibleActions;
    public CatchActionAssociatedData[] PossibleActions => _possibleActions;

    public float GetSpeedBasedOnDistance(float distance)
    {
        float alpha = Mathf.Clamp01(distance / _minSpeedAtDistance);
        return Mathf.Lerp(_maxFallSpeed, _minFallSpeed, _speedReductionCurve.Evaluate(alpha));
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
