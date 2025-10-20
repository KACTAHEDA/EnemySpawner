
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Pool;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<SpawnPoint> _points;
    [SerializeField] private float _interval = 2f;
    [SerializeField] private int _poolCapacity = 50;
    [SerializeField] private int _maxPoolSize = 100;
    private Vector3 _moveDirection;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: (enemy) => enemy.gameObject.SetActive(true),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maxPoolSize);
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void SpawnEnemy()
    {
        int randomPointIndex = Random.Range(0, _points.Count);
        var point = _points[randomPointIndex];

        Vector3 position = point.transform.position;
        _moveDirection = point.transform.forward;

        Enemy enemy = _pool.Get();
        enemy.Init(position, _moveDirection);
        enemy.OnLifeEnd += EnemyIsDead;
    }

    private void EnemyIsDead(Enemy enemy)
    {
        enemy.OnLifeEnd -= EnemyIsDead;
        _pool.Release(enemy);
    }

    private IEnumerator SpawnCoroutine()
    {
        var delay = new WaitForSeconds(_interval);

        while (gameObject.activeInHierarchy)
        {
            SpawnEnemy();
            yield return delay;
        }
    }
}
