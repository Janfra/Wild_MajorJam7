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
        Catchable instance = Instantiate(SelectRandomPrefab(), GetSpawnVector(xPosition), Quaternion.identity);
        instance.OnDrop();
    }

    private Vector3 GetSpawnVector(float xPosition)
    {
        return new Vector3(xPosition, _spawnHeight, _spawnDepth);
    }

    private Catchable SelectRandomPrefab()
    {
        int index = Random.Range(0, _prefabOptions.Length);
        return _prefabOptions[index];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(GetSpawnVector(_spawnXRange.x), GetSpawnVector(_spawnXRange.y));
    }
}
