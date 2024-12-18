using DG.Tweening;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected GameEventChannelSO _spawnChannel;
    [SerializeField] protected PlayerBullet _bullet;
    [SerializeField] private float _recoilTime;
    [SerializeField] private float _recoilXValue;

    [SerializeField] private float _minPower;

    private Vector3 _defaultPos;

    protected Transform _visual;
    protected Transform _fireTrm;
    protected Player _player;
    
    private void Awake()
    {
        _visual = transform.Find("Visual");
        _fireTrm = transform.Find("FirePos");
    }

    public void SetOwner(Player player) => _player = player;

    public virtual void Charging(float chargingTime ,float chargingValue)
    {
        //Debug.Log(chargingValue);
    }

    public virtual void Fire(float power)
    {
        Recoil(power);
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
}
