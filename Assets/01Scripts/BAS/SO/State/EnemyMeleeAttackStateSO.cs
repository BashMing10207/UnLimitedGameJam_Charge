using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
[CreateAssetMenu(fileName = "EnemyRunState", menuName = "SO/EnemyState/EnemyAttackState")]
public class EnemyMeleeAttackStateSO : EnemyStateSO
{
    [SerializeField]
    private AnimStateSO _attackParm;
    public override void OnEnter(Entity entity)
    {
        _entity = entity;
        if (_statModifier != null)
            entity.GetEntityCompo<EntityStat>().AddModifier(_statModifier.TargetStat, _statModifier, _statModifier.Value);
        entity.GetComponentInChildren<Animator>().SetBool(_attackParm.HashValue, true);
    }

    public override void OnExit()
    {
        if (_statModifier != null)
            _entity.GetEntityCompo<EntityStat>().RemoveModifier(_statModifier.TargetStat, _statModifier);
        _entity.GetComponentInChildren<Animator>().SetBool(_attackParm.HashValue, false);
    }

    public override void Update()
    {
        
    }


}
