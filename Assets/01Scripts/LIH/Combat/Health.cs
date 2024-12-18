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

    private float _damage;

    private Entity _owner;
    
    public bool IsDead { get; set; }

    private void Update()
    {
        if (Keyboard.current.nKey.wasPressedThisFrame)
            ApplyDamage(3f);
    }

    public void Initialize(Entity entity)
    {
        _owner = entity;
    }
    
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
}
