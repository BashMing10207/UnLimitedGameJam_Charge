using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRunState", menuName = "SO/EnemyState/EnemyRunState")]
public class EnemyRunState : EnemyStateSO //도망가기 상태
{

    [SerializeField]
    private float _distance = 5f;
    [SerializeField]
    private bool _isPierceObstacle = false;
    [SerializeField]
    private LayerMask _whatisObstacle;
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
        Vector2 targetpos = _playerSO.PlayerTrm.position + (_playerSO.PlayerTrm.position - _entity.transform.position) * _distance; ;
        if(_isPierceObstacle )
           {
            RaycastHit2D hit = Physics2D.Raycast(_playerSO.PlayerTrm.position, (_playerSO.PlayerTrm.position - _entity.transform.position), _distance,_whatisObstacle);
            if(hit)
                targetpos = hit.point;
            } 
        _astar.Target = targetpos;
        Vector2 dir = _astar.PathDir;
        _entity.GetEntityCompo<EnemyMovement>().Move(dir);
    }
}
