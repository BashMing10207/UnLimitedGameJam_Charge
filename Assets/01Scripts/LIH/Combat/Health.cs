using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour, IDamageable, IEntityComponent, IAfterInitable
{
    [SerializeField] private StatSO _healthStat;
    private float _maxHealth;
    private float _currentHealth;

    public UnityEvent<float> OnHit;
    public UnityEvent OnDead;
    public UnityEvent OnInvincibilityEvent;

    private float _damage;

    private Entity _owner;

    private bool _isInvincibility;
    
    public bool IsDead { get; set; }

    private void Update()
    {
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            ApplyDamage(3f);
            Debug.Log(_currentHealth);
        }
    }

    public void Initialize(Entity entity)
    {
        _owner = entity;
    }

    public void SetInvincibility(bool isActive) => _isInvincibility = isActive;
    
    public void AfterInit()
    {
        SetHealth(true);
    }

    private void SetHealth(bool isResetCurrent = false)
    {
        _maxHealth = _owner.GetEntityCompo<EntityStat>().GetStat(_healthStat).Value;
        if (isResetCurrent)
            _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (IsDead)
            return;

        if (_isInvincibility)
        {
            OnInvincibilityEvent?.Invoke();
            return;
        }
        
        _damage = damage;
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        
        if (_currentHealth <= 0)
            IsDead = true;
        
        HitFeedBack();
    }

    private void HitFeedBack()
    {
        if (IsDead)
            OnDead?.Invoke();
        else
            OnHit?.Invoke(_damage);
    }

    public float GetNormalizeHealth() => _currentHealth / _maxHealth;

    public float GetPredictionNormalizeHealth(float damage)
    {
        return Mathf.Clamp(_currentHealth - damage / _maxHealth, 0f, 1f);
    }
}
