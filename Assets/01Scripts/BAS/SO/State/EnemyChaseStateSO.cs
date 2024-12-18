using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
[CreateAssetMenu(fileName = "EnemyRunState", menuName = "SO/EnemyState/EnemyChaseState")]
public class EnemyChaseStateSO : EnemyStateSO // 쫒아가기 상태
{

    private BashAstar _astar;


    public override void OnEnter(Entity entity)
    {
        _entity = entity;
        
        entity.GetEntityCompo<EntityStat>().AddModifier(_statModifier.TargetStat,_statModifier,_statModifier.Value);
        _astar = _entity.GetEntityCompo<BashAstar>();
    }

    public override void OnExit()
    {
        _entity.GetEntityCompo<EntityStat>().RemoveModifier(_statModifier.TargetStat, _statModifier);
    }

    public override void Update()
    {
        Debug.Log(_entity.gameObject.name);
        var astar = _entity.GetEntityCompo<BashAstar>();

        _astar.Target = _playerSO.PlayerTrm.position;
        Vector2 dir = astar.PathDir;
        _entity.GetEntityCompo<EnemyMovement>().Move(dir);
    }
}
