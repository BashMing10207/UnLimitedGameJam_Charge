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
    private PlayerWeaponController _playerWeaponController;
    private Rigidbody2D _rigidbody2D;
    private EntityStat _statCompo;

    private bool _canMove = true;

    private Vector2 _moveDir;

    private float _moveSpeed;
    private float _dashSpeed;
    private float _currentDashCool;
    private float _dashCoolTime;

    private float _chargingMoveMultiplier = 1f;

    public void Initialize(Player player)
    {
        _player = player;
        _playerWeaponController = player.GetPlayerCompo<PlayerWeaponController>();
        _rigidbody2D = player.GetComponent<Rigidbody2D>();
        _statCompo = _player.GetEntityCompo<EntityStat>();
        
        _moveSpeed = _statCompo.GetStat(_moveSpeedStat).Value;
        _dashSpeed = _statCompo.GetStat(_dashSpeedStat).Value;
        _dashCoolTime = _statCompo.GetStat(_dashCoolStat).Value;
        
        _player.PlayerInput.DashEvent += HandleDashEvent;
        _playerWeaponController.chargingEvent.AddListener(HandleSetChargingSpeed);
        _playerWeaponController.fireEvent.AddListener(HandleResetChargingSpeed);
    }

    private void OnDestroy()
    {
        _player.PlayerInput.DashEvent -= HandleDashEvent;
        _playerWeaponController.chargingEvent.RemoveListener(HandleSetChargingSpeed);
        _playerWeaponController.fireEvent.RemoveListener(HandleResetChargingSpeed);
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

        _rigidbody2D.linearVelocity = _player.LookDir() * _dashSpeed;
    
        DOVirtual.DelayedCall(_dashTime, () => _canMove = true);
    }

    private void FixedUpdate()
    {
        if(!_canMove)
            return;
        
        _moveDir = _player.PlayerInput.InputDirection;
        _rigidbody2D.linearVelocity = _moveDir * _moveSpeed / _chargingMoveMultiplier;
    }
    
    private void StopImmediately(bool isYAxisToo = false)
    {
        if(isYAxisToo)
            _rigidbody2D.linearVelocity = Vector2.zero;
        else
            _rigidbody2D.linearVelocityX = 0;

        _moveDir = Vector2.zero;
    }
    
    private void HandleResetChargingSpeed() => _chargingMoveMultiplier = 1f;
    private void HandleSetChargingSpeed(float chargingValue) => _chargingMoveMultiplier = Mathf.Max(1,chargingValue * 0.1f);
}
