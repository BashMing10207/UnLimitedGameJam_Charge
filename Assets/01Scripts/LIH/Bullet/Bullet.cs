using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolType _poolType;
    public GameObject GameObject => gameObject;
    public PoolType Type => _poolType;

    [SerializeField] protected float _lifeTime;
    [SerializeField] protected float _defaultBulletSpeed;
    [SerializeField] protected LayerMask whatIsTarget;
    protected Rigidbody2D _rigidbody2D;

    protected float _power;

    protected Pool _myPool;

    protected float _currentTime;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void Shoot(Vector2 dir, float power, float speed)
    {
        _power = Mathf.RoundToInt(power);
        _rigidbody2D.AddForce(dir * _defaultBulletSpeed * speed, ForceMode2D.Impulse);
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
