using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float _defaultBulletSpeed;
    private Rigidbody2D _rigidbody2D;

    private float _power;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 dir, float power)
    {
        _power = Mathf.RoundToInt(power);
        _rigidbody2D.AddForce(dir * _defaultBulletSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(_power);
        }
        Destroy(gameObject);
    }
}
