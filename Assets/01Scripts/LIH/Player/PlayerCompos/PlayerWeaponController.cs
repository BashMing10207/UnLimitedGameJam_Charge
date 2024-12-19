using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponController : MonoBehaviour, IPlayerCompo
{
    public UnityEvent<float, float> chargingEvent;
    public UnityEvent<float> fireEvent;
    public UnityEvent resetEvent;

    [SerializeField] private StatSO _chargingSpeedStat;
    [SerializeField] private float _minChargingValue;
    
    private Player _player;
    private PlayerInputSO _playerInput;
    private PlayerRender _playerRender;
    
    private EntityStat _statCompo;

    public Weapon currentWeapon;

    private Vector2 _lookDir;
    private bool _isChargingStart;

    private float _chargingSpeed;
    private float _currentCharging;
    private float _currentChargingTime;
    
    public void Initialize(Player player)
    {
        _player = player;
        _playerInput = player.PlayerInput;
        _playerRender = player.GetPlayerCompo<PlayerRender>();
        _statCompo = player.GetEntityCompo<EntityStat>();

        _chargingSpeed = _statCompo.GetStat(_chargingSpeedStat).Value;

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
        }
    }

    private void Update()
    {
        if (_isChargingStart)
        {
            _currentChargingTime += Time.deltaTime;
            _currentCharging += Time.deltaTime * _chargingSpeed;
            chargingEvent?.Invoke(_currentChargingTime, _currentCharging);
//            Debug.Log(_currentCharging);
        }
        
        GunRotate();
    }
    
    private void GunRotate()
    {
        _lookDir = _player.LookDir();
        float z = Mathf.Atan2(_lookDir.y, _lookDir.x) * Mathf.Rad2Deg;
        Debug.Log(z);
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
