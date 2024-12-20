using UnityEngine;
using UnityEngine.Events;

public abstract class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolType _poolType;
    public GameObject GameObject => gameObject;
    public PoolType Type => _poolType;

    [SerializeField] protected float _lifeTime;
    [SerializeField] protected float _defaultBulletSpeed;
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] protected float _delateTime = 0.4f;
    protected Rigidbody2D _rigidbody2D;

    [SerializeField]
    protected float _power;

    protected Pool _myPool;

    protected float _currentTime;

    public UnityEvent<float> BulletInit;

    [SerializeField] TrailRenderer _trailRenderer;
    protected bool _isActiving = true;

    private void OnEnable()
    {
        _isActiving = true;

    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void Shoot(Vector2 dir, float power, float speed)
    {
        _power = Mathf.RoundToInt(power);
        _rigidbody2D.AddForce(dir * _defaultBulletSpeed * speed, ForceMode2D.Impulse);
        BulletInit?.Invoke(power);
        if (_trailRenderer != null)
            _trailRenderer.SetPositions(new Vector3[0] { });
    }

    private void Update()
    {
        if (_isActiving)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _lifeTime)
            {
                _myPool.Push(this);
                _isActiving = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_isActiving)
        {
            Transform root = other.transform.root;

            if ((whatIsTarget & (1 << other.gameObject.layer)) != 0)
            {
                if (root.TryGetComponent(out IDamageable health))
                {
                    health.ApplyDamage(_power);
                    Invoke(nameof(PoolBullet), _delateTime);
                    _isActiving = false;
                    _rigidbody2D.linearVelocity = Vector2.zero;
                }
            }
        }
        
    }

    private void PoolBullet()
    {
        _myPool.Push(this);
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
