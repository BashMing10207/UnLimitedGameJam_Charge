using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponController : MonoBehaviour, IPlayerCompo
{
    public UnityEvent<float> chargingEvent;
    public UnityEvent fireEvent;
    
    [SerializeField] private StatSO _chargingSpeedStat;
    
    private Player _player;
    private PlayerInputSO _playerInput;
    
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
            _currentCharging = 0f;
            fireEvent?.Invoke();
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
        _lookDir = _playerInput.MousePos - (Vector2)transform.position;
        float z = Mathf.Atan2(_lookDir.y, _lookDir.x) * Mathf.Rad2Deg;
        currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(0,0, z));
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
