using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRunState", menuName = "SO/EnemyState/EnemyRunState")]
public class EnemyRunState : EnemyStateSO //�������� ����
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
        Debug.Log("���� �÷��̾� ��ġ �Ҵ� ���߾�!");
        Vector2 dir = _astar.PathDir;
        _entity.GetEntityCompo<EnemyMovement>().Move(dir);
    }
}
