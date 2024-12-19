using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerMuzzleEffect _playerMuzzleEffect;
    [SerializeField] private ParticleEmisiionHandler _emisiion;
    [SerializeField] protected GameEventChannelSO _spawnChannel;
    [SerializeField] protected PlayerBullet _bullet;

    [SerializeField] private float _particleMaxSize;
    [SerializeField] private float _recoilTime;
    [SerializeField] private float _recoilXValue;

    [SerializeField] private float _minPower;

    private Vector3 _defaultPos;

    protected Transform _visual;
    protected Transform _fireTrm;
    protected Player _player;

    private ParticleEmisiionHandler _currentEmisiion;
    
    private void Awake()
    {
        _visual = transform.Find("Visual");
        _fireTrm = transform.Find("FirePos");
    }

    public void SetOwner(Player player)
    {
        _player = player;
        
        _player.GetPlayerCompo<PlayerWeaponController>().resetEvent.AddListener(HandleEmmisionReset);
    }

    private void OnDestroy()
    {
        _player.GetPlayerCompo<PlayerWeaponController>().resetEvent.RemoveListener(HandleEmmisionReset);
    }
    
    public virtual void Charging(float chargingTime ,float chargingValue)
    {
        
    }

    public virtual void Fire(float power)
    {
        InstantiateMuzzle();
        Recoil(power);
    }

    private void InstantiateMuzzle()
    {
        PlayerMuzzleEffect effect = Instantiate(_playerMuzzleEffect);
        effect.transform.position = _fireTrm.position;
        float z = Mathf.Atan2(_player.LookDir().y, _player.LookDir().x) * Mathf.Rad2Deg;
        effect.SetRotate(z);
    }

    private void Recoil(float power)
    {
        if(power <= _minPower)
            return;
        
        _defaultPos = _visual.localPosition;

        Sequence seq = DOTween.Sequence();
        seq.Append(_visual.DOLocalMoveX(_recoilXValue, _recoilTime));
        seq.Append(_visual.DOLocalMoveX(_defaultPos.x, _recoilTime))
            .OnComplete(() => _visual.localPosition = _defaultPos);
    }
    
    private void HandleEmmisionReset()
    {
        if(_currentEmisiion == null)
            return;

        _currentEmisiion.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(x 
            => x.loop = false);
    }
}
