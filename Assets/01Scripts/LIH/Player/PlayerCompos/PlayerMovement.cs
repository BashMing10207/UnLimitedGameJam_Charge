using System;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerCompo
{
    [SerializeField] private StatSO _moveSpeedStat;
    [SerializeField] private StatSO _dashSpeedStat;
    [SerializeField] private StatSO _dashCoolStat;
    [SerializeField] private float _dashTime;
    
    private Player _player;
    private Rigidbody2D _rigidbody2D;
    private EntityStat _statCompo;

    private bool _canMove = true;

    private Vector2 _moveDir;
    
    private float _moveSpeed;
    private float _dashSpeed;
    private float _currentDashCool;
    private float _dashCoolTime;

    public void Initialize(Player player)
    {
        _player = player;
        _rigidbody2D = player.GetComponent<Rigidbody2D>();
        _statCompo = _player.GetEntityCompo<EntityStat>();
        
        _moveSpeed = _statCompo.GetStat(_moveSpeedStat).Value;
        _dashSpeed = _statCompo.GetStat(_dashSpeedStat).Value;
        _dashCoolTime = _statCompo.GetStat(_dashCoolStat).Value;
        
        _player.PlayerInput.DashEvent += HandleDashEvent;
    }

    private void OnDestroy()
    {
        _player.PlayerInput.DashEvent -= HandleDashEvent;
    }

    private void Update()
    {
        if (_currentDashCool >= 0)
            _currentDashCool -= Time.deltaTime;
    }

    private void HandleDashEvent()
    {
        if(_currentDashCool >= 0)
            return;
        
        _canMove = false;
        _currentDashCool = _dashCoolTime;
        StopImmediately(true);

        _moveDir = _player.PlayerInput.MousePos - (Vector2)transform.position;
        _rigidbody2D.linearVelocity = _moveDir.normalized * _dashSpeed;
    
        DOVirtual.DelayedCall(_dashTime, () => _canMove = true);
    }

    private void FixedUpdate()
    {
        if(!_canMove)
            return;
        
        _moveDir = _player.PlayerInput.InputDirection;
        _rigidbody2D.linearVelocity = _moveDir * _moveSpeed;
    }
    
    private void StopImmediately(bool isYAxisToo = false)
    {
        if(isYAxisToo)
            _rigidbody2D.linearVelocity = Vector2.zero;
        else
            _rigidbody2D.linearVelocityX = 0;

        _moveDir = Vector2.zero;
    }
}
