using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRunState", menuName = "SO/EnemyState/EnemyRunState")]
public class EnemyRunState : EnemyStateSO //도망가기 상태
{

    [SerializeField]
    private StatModifierSO _statModifier;
    [SerializeField]
    private float _distance = 5f;

    private BashAstar _astar;
    public override void OnEnter(Entity entity)
    {
        _entity = entity;

        if( _statModifier != null )
        entity.GetEntityCompo<EntityStat>().AddModifier(_statModifier.TargetStat, _statModifier, _statModifier.Value);

        _astar = _entity.GetEntityCompo<BashAstar>();
    }

    public override void OnExit()
    {
        if (_statModifier != null)
            _entity.GetEntityCompo<EntityStat>().RemoveModifier(_statModifier.TargetStat, _statModifier);
    }

    public override void Update()
    {
        //_astar.Target = playerpos + (playerpos - _entity.transform.position)*_distance;
        Debug.Log("아직 플레이어 위치 할당 안했어!");
        Vector2 dir = _astar.PathDir;
        _entity.GetEntityCompo<EnemyMovement>().Move(dir);
    }
}
