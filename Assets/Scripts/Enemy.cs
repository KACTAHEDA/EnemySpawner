using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private float _maxLifeTime = 15f;
    private float _lifetime;
    private Vector3 _moveDirection;

    public event Action<Enemy> OnLifeEnd;

    private void OnEnable()
    {
        _lifetime = _maxLifeTime;
    }

    private void Update()
    {
        _lifetime -= Time.deltaTime;

        if (_lifetime <= 0f)
        {
            OnLifeEnd?.Invoke(this);
        }
        else
        {
            Move();
        }
    }

    public void Init(Vector3 position, Vector3 moveDirection)
    {
        transform.position = position;
        _moveDirection = moveDirection;
        transform.forward = _moveDirection;
    }

    private void Move()
    {
        transform.position += _moveDirection * _speed * Time.deltaTime ;
    }
}
