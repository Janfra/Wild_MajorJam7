using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    [Header("Enable Configuration")]
    [SerializeField]
    private bool _isEnabledOnStart = false;

    [SerializeField]
    private GameEvent _enableOnEvent;

    [Header("Spawn Configuration")]
    [SerializeField]
    private Vector2 _spawnXRange;
    
    [SerializeField]
    private float _spawnHeight;

    [SerializeField]
    private float _spawnDepth;

    [SerializeField]
    private Catchable[] _prefabOptions;

    [SerializeField]
    private LevelUpNotifier _levelUpNotifier;

    [SerializeField]
    private BasicTimer _spawnTimer;

    private LevelSpawnData _currentSpawnData;
    private float _lastXSpawnPosition;

    private void Awake()
    {
        VerifyArray(_prefabOptions);

        _spawnTimer.SubscribeToCallback(Spawn);

        if (_enableOnEvent)
        {
            _enableOnEvent.OnEvent += EnableSpawner;
        }

        if (_levelUpNotifier)
        {
            _levelUpNotifier.OnLevelUp += UpdateSpawnSettings;
        }
    }

    private void UpdateSpawnSettings(LevelData newLevel)
    {
        _currentSpawnData = newLevel.SpawnData;
        _spawnTimer.TargetDuration = _currentSpawnData.SpawnDelay;
        _spawnTimer.ResetProgress();
        VerifyArray(_currentSpawnData.PossibleActions);
    }

    private void Start()
    {
        DisableSpawner();
    }

    private void Update()
    {
        _spawnTimer.IsTimerTickOnTarget(false);
    }

    private void EnableSpawner()
    {
        gameObject.SetActive(true);
    }

    private void DisableSpawner()
    {
        gameObject.SetActive(false);
    }

    private void Spawn()
    {
        float xPosition = Random.Range(_spawnXRange.x, _spawnXRange.y);
        Catchable instance = Instantiate(GetRandomItemFromArray(_prefabOptions), GetSpawnVector(xPosition), Quaternion.identity);
        instance.OnDrop(GetRandomItemFromArray(_currentSpawnData.PossibleActions), GetFallSpeed(xPosition));
        _lastXSpawnPosition = xPosition;
    }

    private Vector3 GetSpawnVector(float xPosition)
    {
        return new Vector3(xPosition, _spawnHeight, _spawnDepth);
    }

    private T GetRandomItemFromArray<T>(T[] array)
    {
        int index = Random.Range(0, array.Length);
        return array[index];
    }

    private float GetFallSpeed(float xSpawnPosition)
    {
        float rv = _currentSpawnData.MinFallSpeed;

        if (_currentSpawnData)
        {
            return _currentSpawnData.GetSpeedBasedOnDistance(Mathf.Abs(xSpawnPosition - _lastXSpawnPosition));
        }

        return rv;
    }

    private void VerifyArray<T>(T[] array)
    {
        if (array.Length == 0)
        {
            throw new System.NullReferenceException($"Please add items to the {array} array in {name}");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(GetSpawnVector(_spawnXRange.x), GetSpawnVector(_spawnXRange.y));
    }
}
