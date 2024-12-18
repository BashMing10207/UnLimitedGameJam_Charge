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
    private float _dashSpeed = 100;
    //private StatModifierSO _dashSpeed;
    void Start()
    {
        var fsm = GetEntityCompo<EnemyFSM>();
        fsm.enabled = false;
        fsm.SetState(_idle);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(Mathf.Abs(Vector2.SignedAngle((_playerManager.PlayerTrm.position - transform.position),(_playerManager.PlayerTrm.position - Camera.main.ScreenToWorldPoint(Mouse.current.position.value)))) < _activeDegree)
        {
            _currentActiveTime =_activingTime;
            Debug.Log("aaaa");
        }

        if (_currentActiveTime > 0)
        {
            _currentActiveTime -= Time.deltaTime;

            var fsm = GetEntityCompo<EnemyFSM>();

            if(!fsm.enabled)
            {
                fsm.enabled = true;
                fsm.SetState(_battlebeginner);
            }
        }
        else
        {
            var fsm = GetEntityCompo<EnemyFSM>();
            if (fsm.enabled)
            {
                fsm.enabled = false;
                fsm.SetState(_idle);
            }
        }
    }

    // 이 아래는 애니메이션 트리거 날먹
    public void DashAttack()
    {
        GetEntityCompo<EnemyMovement>().Knockback(( _playerManager.PlayerTrm.position- transform.position).normalized*_dashSpeed);
    }
    public void DashAttackEnd()
    {
        GetEntityCompo<EnemyMovement>().SetVelocity(Vector2.zero);
    }
}
