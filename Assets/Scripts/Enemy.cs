
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private float _maxLifeTime = 15f;
    private float _lifetime;

    private System.Action<Enemy> _onLifeEnd;

    public void Init(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void InitCollback(System.Action<Enemy> onLifeEnd)
    {
        _onLifeEnd = onLifeEnd;
    }

    private void OnEnable()
    {
        _lifetime = _maxLifeTime;
    }

    private void Update()
    {
        _lifetime -= Time.deltaTime;

        if(_lifetime <= 0f)
        {
            _onLifeEnd?.Invoke(this);
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position += transform.forward * _speed * Time.deltaTime ;
    }
}
