using DG.Tweening;
using UnityEngine;

public class PlayerHealthBar : HudBarUI
{
    [SerializeField] private float _shakeValue;
    [SerializeField] private PlayerManagerSO _playerManagerSo;
    private Player _player;

    private Vector3 _defaultPos;
    
    private void Start()
    {
        _player = _playerManagerSo.Player;

        _player.GetEntityCompo<Health>().OnDead.AddListener(HandleDisableEvent);
        _player.GetEntityCompo<Health>().OnHit.AddListener(HandleFillEvent);

        _defaultPos = transform.position;
    }

    protected override void HandleDisableEvent()
    {
    }

    protected override void HandleFillEvent(float damage)
    {
        transform.DOShakePosition(0.1f, _shakeValue).OnComplete(() => transform.position = _defaultPos);
        
        float fillAmount = _player.GetEntityCompo<Health>().GetNormalizeHealth();
        _fillImage.DOFillAmount(fillAmount, 0.3f);
    }
}
