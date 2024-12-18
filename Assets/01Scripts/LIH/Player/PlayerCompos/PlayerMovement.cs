using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour, IPlayerCompo
{
    [Header("Stat")]
    [SerializeField] private StatSO _moveSpeedStat;
    [SerializeField] private StatSO _dashSpeedStat;
    [SerializeField] private StatSO _dashCoolStat;

    [Header("Dash Setting")]
    [SerializeField] private float _dashTime;
    
    [Header("Knockback Setting")]
    [SerializeField] private float _knockBackTime;
    [SerializeField] private float _applyKnockBackMaxPower;
    [SerializeField] private float _maxKnockBackChargingValue;
    [SerializeField] private float _knockBackMinPower;
    
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
    
    public void KnockBack(float power)
    {
        if(power <= _knockBackMinPower)
            return;
            
        _canMove = false;
        StopImmediately(true);
        
        float inverseLerp = Mathf.InverseLerp(0, _maxKnockBackChargingValue, power);
        float knockBackPower = Mathf.Lerp(0, _applyKnockBackMaxPower, inverseLerp);
        
        _rigidbody2D.AddForce(-_player.LookDir() * knockBackPower, ForceMode2D.Impulse);
        DOVirtual.DelayedCall(_knockBackTime, () => _canMove = true);
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
    
    private void HandleResetChargingSpeed(float power) => _chargingMoveMultiplier = 1f;
    private void HandleSetChargingSpeed(float chargingValue) => _chargingMoveMultiplier = Mathf.Max(1,chargingValue * 0.1f);
}
