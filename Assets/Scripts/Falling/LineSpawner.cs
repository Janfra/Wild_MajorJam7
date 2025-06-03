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
    private CatchActionAssociatedData[] _possibleActions;

    [SerializeField]
    private BasicTimer _spawnTimer;

    private void Awake()
    {
        VerifyArray(_prefabOptions);
        VerifyArray(_possibleActions);

        _spawnTimer.SubscribeToCallback(Spawn);

        if (_enableOnEvent)
        {
            _enableOnEvent.OnEvent += EnableSpawner;
        }
    }

    private void Start()
    {
        if (!_isEnabledOnStart)
        {
            DisableSpawner();
        }
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
        instance.OnDrop(GetRandomItemFromArray(_possibleActions));
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
