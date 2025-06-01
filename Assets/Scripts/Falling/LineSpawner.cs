using UnityEngine;

public class LineSpawner : MonoBehaviour
{
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
    }

    private void Start()
    {
        _spawnTimer.SubscribeToCallback(Spawn);
    }

    private void Update()
    {
        _spawnTimer.IsTimerTickOnTarget(false);
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
