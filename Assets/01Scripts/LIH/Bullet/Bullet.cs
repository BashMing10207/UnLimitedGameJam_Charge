using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolType _poolType;
    public GameObject GameObject => gameObject;
    public PoolType Type => _poolType;

    [SerializeField] private float _lifeTime;
    [SerializeField] private float _defaultBulletSpeed;
    [SerializeField] private LayerMask whatIsTarget;
    private Rigidbody2D _rigidbody2D;

    private float _power;

    private Pool _myPool;

    private float _currentTime;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void Shoot(Vector2 dir, float power)
    {
        _power = Mathf.RoundToInt(power);
        _rigidbody2D.AddForce(dir * _defaultBulletSpeed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _lifeTime)
        {
            _myPool.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform root = other.transform.root;
        
        if ((whatIsTarget & (1 << other.gameObject.layer)) != 0)
        {
            if (root.TryGetComponent(out IDamageable health))
            {
                health.ApplyDamage(_power);
            }
            _myPool.Push(this);
        }
    }

    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }

    public void ResetItem()
    {
        _currentTime = 0f;
    }
}
