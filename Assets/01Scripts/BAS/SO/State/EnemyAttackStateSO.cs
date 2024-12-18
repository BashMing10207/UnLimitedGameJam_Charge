using UnityEngine;

public class EnemyAttackStateSO : EnemyStateSO
{
    [SerializeField]
    private StatModifierSO _statModifier;

    public override void OnEnter(Entity entity)
    {
        _entity = entity;
        if (_statModifier != null)
            entity.GetEntityCompo<EntityStat>().AddModifier(_statModifier.TargetStat, _statModifier, _statModifier.Value);

    }

    public override void OnExit()
    {
        if (_statModifier != null)
            _entity.GetEntityCompo<EntityStat>().RemoveModifier(_statModifier.TargetStat, _statModifier);
    }

    public override void Update()
    {
        
    }
}
