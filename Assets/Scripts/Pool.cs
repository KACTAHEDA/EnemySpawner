using UnityEngine.Pool;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _defaultCapacity = 50;
    [SerializeField] private int _maxSize = 100;

    private IObjectPool<Enemy> _pool;

    public Enemy GetEnemy() => _pool.Get();

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(CreateEnemy, OnTakeFromPool, OnReturnToPool, OnDestroyEnemy, true, _defaultCapacity, _maxSize);
    }

    private Enemy CreateEnemy()
    {       
        var enemy = Instantiate(_enemyPrefab);
        enemy.InitCollback(ReturnToPool);
        return enemy;
    }

    private void OnTakeFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    
    private void OnReturnToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    private void ReturnToPool(Enemy enemy) => _pool.Release(enemy);
}
