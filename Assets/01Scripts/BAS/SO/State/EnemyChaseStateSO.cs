using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
[CreateAssetMenu(fileName = "EnemyRunState", menuName = "SO/EnemyState/EnemyChaseState")]
public class EnemyChaseStateSO : EnemyStateSO // �i�ư��� ����
{
    [SerializeField]
    private StatModifierSO _statModifier;

    public override void OnEnter(Entity entity)
    {
        _entity = entity;
        
        entity.GetEntityCompo<EntityStat>().AddModifier(_statModifier.TargetStat,_statModifier,_statModifier.Value);
    }

    public override void OnExit()
    {
        _entity.GetEntityCompo<EntityStat>().RemoveModifier(_statModifier.TargetStat, _statModifier);
    }

    public override void Update()
    {
        var astar = _entity.GetEntityCompo<BashAstar>();
        //_astar.Target = playerpos;
        Debug.Log("���� �÷��̾� ��ġ �Ҵ� ���߾�!");
        Vector2 dir = astar.PathDir;
        _entity.GetEntityCompo<EnemyMovement>().Move(dir);
    }
}
