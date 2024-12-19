using UnityEngine;
using UnityEngine.InputSystem;

public class SecondBoss : Enemy
{
    [SerializeField]
    private PlayerManagerSO _playerManager;
    private float _currentActiveTime = 0;
    [SerializeField]
    private float _activeDegree = 5,_activingTime=8;

    [SerializeField]
    private EnemyStateSO _idle, _battlebeginner;
    [SerializeField]
    private float _dashSpeed = 100,_activeDistance=1f;
    [SerializeField]
    private LayerMask _whatisObstacle;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private bool _isFilpable=true;
    //private StatModifierSO _dashSpeed;
    protected override void Awake()
    {   
        base.Awake();
        var fsm = GetEntityCompo<EnemyFSM>();
        //fsm.enabled = false;
        fsm.SetState(_idle);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(Mathf.Abs(Vector2.SignedAngle((_playerManager.PlayerTrm.position - transform.position),
            (_playerManager.PlayerTrm.position - Camera.main.ScreenToWorldPoint(Mouse.current.position.value)))) < _activeDegree
            || Vector2.Distance(transform.position,_playerManager.PlayerTrm.position)<_activeDistance)
        {
            if(!Physics2D.Raycast(transform.position,_playerManager.PlayerTrm.position - transform.position, Vector2.Distance(_playerManager.PlayerTrm.position, transform.position),_whatisObstacle))
            _currentActiveTime =_activingTime;
        }

        if (_currentActiveTime > 0)
        {
            _currentActiveTime -= Time.deltaTime;

            var fsm = GetEntityCompo<EnemyFSM>();

            //if(!fsm.enabled)
            if(fsm.CurrentState == _idle)
            {
                fsm.SetState(_battlebeginner);
                //fsm.enabled = true;
            }
        }
        else
        {
            var fsm = GetEntityCompo<EnemyFSM>();
            if (fsm.CurrentState != _idle)
            {
                fsm.SetState(_idle);
                //fsm.enabled = false;
            }
        }

        if(_isFilpable)
        {
            _spriteRenderer.flipX = _playerManager.PlayerTrm.position.x < transform.position.x;
        }
    }

    // 이 아래는 애니메이션 트리거 날먹
    public void DashAttack()
    {
        Vector2 dir = (_playerManager.PlayerTrm.position - transform.position);
        GetEntityCompo<EnemyMovement>().Knockback(dir.normalized*(_dashSpeed + dir.magnitude));
        _isFilpable = false;
    }
    public void DashAttackEnd()
    {
        GetEntityCompo<EnemyMovement>().SetVelocity(Vector2.zero);
        _isFilpable = true;
    }
    public void StateEnd()
    {
        GetEntityCompo<EnemyFSM>().StateEnd();
        //_isFilpable = true;
    }
}
