using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("SpawnPoints")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private EnemyData _enemyData;

    private Dictionary<EnemyBrain, Queue<EnemyBrain>> _pools = new Dictionary<EnemyBrain, Queue<EnemyBrain>>();
    private readonly int _poolSizePerType = 20;

    private void OnEnable()
    {
        EnemyBrain.OnBrainDied += ReturnEnemyToPool;
    }

    private void OnDisable()
    {
        EnemyBrain.OnBrainDied -= ReturnEnemyToPool;
    }

    public void SpawnEnemy(EnemyBrain prefab)
    {
        if (!_pools.TryGetValue(prefab, out Queue<EnemyBrain> pool))
        {
            pool = CreatePoolFor(prefab);
        }

        if (pool.Count == 0) return;

        EnemyBrain enemy = pool.Dequeue();
        enemy.transform.SetParent(null);
        enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        enemy.gameObject.SetActive(true);
        enemy.Init(_playerPosition, _enemyData);
    }

    private Queue<EnemyBrain> CreatePoolFor(EnemyBrain prefab)
    {
        Queue<EnemyBrain> pool = new Queue<EnemyBrain>();
        for (int i = 0; i < _poolSizePerType; i++)
        {
            EnemyBrain enemy = Instantiate(prefab, transform);
            enemy.SetSourcePrefab(prefab);
            enemy.gameObject.SetActive(false);
            pool.Enqueue(enemy);
        }

        _pools[prefab] = pool;
        return pool;
    }

    private void ReturnEnemyToPool(EnemyBrain enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(transform);

        if (_pools.TryGetValue(enemy.SourcePrefab, out Queue<EnemyBrain> pool))
        {
            pool.Enqueue(enemy);
        }
    }
}