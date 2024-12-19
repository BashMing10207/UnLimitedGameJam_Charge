using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponController : MonoBehaviour, IPlayerCompo
{
    public UnityEvent<float, float> chargingEvent;
    public UnityEvent<float> fireEvent;
    public UnityEvent resetEvent;

    public List<float> levelSeconds;
    public List<float> levelAdditiveValue;

    [SerializeField] private float _minChargingValue;
    [SerializeField] private float _chargingDelayTime;
    
    public Weapon currentWeapon;
    
    private Player _player;
    private PlayerInputSO _playerInput;
    private PlayerRender _playerRender;
    
    private EntityStat _statCompo;

    private Vector2 _lookDir;
    private bool _isChargingStart;

    private float _chargingSpeed;
    private float _currentCharging;
    private float _currentChargingTime;
    private float _currentChargingDelayTime;
    public int CurrentLevelIndex { get; private set; }
    
    public void Initialize(Player player)
    {
        _player = player;
        _playerInput = player.PlayerInput;
        _playerRender = player.GetPlayerCompo<PlayerRender>();
        _statCompo = player.GetEntityCompo<EntityStat>();

        currentWeapon.SetOwner(_player);
        fireEvent.AddListener(currentWeapon.Fire);
        chargingEvent.AddListener(currentWeapon.Charging);
        
        _playerInput.AttackEvent += HandleChargingEvent;
    }

    private void OnDestroy()
    {
        _playerInput.AttackEvent -= HandleChargingEvent;
    }

    private void HandleChargingEvent(bool isCharging)
    {
        _isChargingStart = isCharging;
        if (!isCharging)
        {
            if (_currentCharging <= _minChargingValue)
                resetEvent?.Invoke();
            else
                fireEvent?.Invoke(_currentCharging);
            
            _currentChargingTime = 0f;
            _currentCharging = 0f;
            _currentChargingDelayTime = 0f;
        }
    }

    private void Update()
    {
        if (_isChargingStart)
        {
            ChargingLogic();
            chargingEvent?.Invoke(_currentChargingTime, _currentCharging);
        }
        
        GunRotate();
    }

    private void ChargingLogic()
    {
        _currentChargingTime += Time.deltaTime;
        float maxValue = levelSeconds.Where(x => x <= _currentChargingTime).Max();
        CurrentLevelIndex = levelSeconds.FindLastIndex(x => x <= maxValue);

        _currentChargingDelayTime -= Time.deltaTime;
        if (_currentChargingDelayTime <= 0)
        {
            _currentCharging += levelAdditiveValue[CurrentLevelIndex];
            _currentChargingDelayTime = _chargingDelayTime;
        }
    }
    
    private void GunRotate()
    {
        _lookDir = _player.LookDir();
        float z = Mathf.Atan2(_lookDir.y, _lookDir.x) * Mathf.Rad2Deg;
        if (_playerRender.FacingDirection <= 0f)
            z = -z + 180f;
        
        currentWeapon.transform.localEulerAngles = new Vector3(0,0, z);
    }

    private void WeaponChange(Weapon weapon)
    {
        fireEvent.RemoveListener(currentWeapon.Fire);
        chargingEvent.RemoveListener(currentWeapon.Charging);
        
        currentWeapon = weapon;
        
        currentWeapon.SetOwner(_player);
        fireEvent.AddListener(currentWeapon.Fire);
        chargingEvent.AddListener(currentWeapon.Charging);
    }
}
