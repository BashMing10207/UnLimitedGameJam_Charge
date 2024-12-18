using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable, IEntityComponent, IAfterInitable
{
    [SerializeField] private StatSO _healthStat;
    private float _maxHealth;
    private float _currentHealth;

    public UnityEvent OnHit;
    public UnityEvent OnDead;

    private Entity _owner;
    
    public bool IsDead { get; set; }
    
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
            OnHit?.Invoke();
    }
}
