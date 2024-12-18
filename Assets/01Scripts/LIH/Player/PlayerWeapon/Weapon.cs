using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected PlayerBullet _bullet;
    
    protected float _currentCharging;
    protected Transform _fireTrm;

    protected Player _player;
    
    private void Awake()
    {
        _fireTrm = transform.Find("FirePos");
    }

    public void SetOwner(Player player) => _player = player;

    public virtual void Charging(float chargingValue)
    {
        Debug.Log(chargingValue);
    }

    public virtual void Fire()
    {
    }
}
