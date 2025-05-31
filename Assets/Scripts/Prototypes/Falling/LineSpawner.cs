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
    private Rigidbody _prefab;

    [SerializeField]
    private BasicTimer _spawnTimer;

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
        Rigidbody instance = Instantiate(_prefab, GetSpawnVector(xPosition), Quaternion.identity);
        instance.useGravity = true;
    }

    private Vector3 GetSpawnVector(float xPosition)
    {
        return new Vector3(xPosition, _spawnHeight, _spawnDepth);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(GetSpawnVector(_spawnXRange.x), GetSpawnVector(_spawnXRange.y));
    }
}
