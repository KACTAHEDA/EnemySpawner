
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool _pool;
    [SerializeField] private List<SpawnPoint> _points;
    [SerializeField] private float _interval = 2f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _interval)
        {
            CreateEnemy();
            _timer = 0f;
        }
    }

    private void CreateEnemy()
    {
        int randomPointIndex = Random.Range(0, _points.Count);
        var point = _points[randomPointIndex];

        Vector3 position = point.transform.position;
        Quaternion rotation = point.transform.rotation;

        Enemy enemy = _pool.GetEnemy();
        enemy.Init(position, rotation);
    }
}
