using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponController : MonoBehaviour, IPlayerCompo
{
    public UnityEvent<float> chargingEvent;
    public UnityEvent<float> fireEvent;

    [SerializeField] private StatSO _chargingSpeedStat;
    
    private Player _player;
    private PlayerInputSO _playerInput;
    private PlayerRender _playerRender;
    
    private EntityStat _statCompo;

    public Weapon currentWeapon;

    private Vector2 _lookDir;
    private bool _isChargingStart;

    private float _chargingSpeed;
    private float _currentCharging;
    
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
            fireEvent?.Invoke(_currentCharging);
            _currentCharging = 0f;
        }
    }

    private void Update()
    {
        if (_isChargingStart)
        {
            _currentCharging += Time.deltaTime * _chargingSpeed;
            chargingEvent?.Invoke(_currentCharging);
        }
        
        GunRotate();
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
