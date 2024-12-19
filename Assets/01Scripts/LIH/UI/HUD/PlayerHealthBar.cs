using System;
using DG.Tweening;
using UnityEngine;

public class PlayerHealthBar : HudBarUI
{
    [SerializeField] private PlayerManagerSO _playerManagerSo;
    private Player _player;
    
    private void Start()
    {
        _player = _playerManagerSo.Player;

        _player.GetEntityCompo<Health>().OnDead.AddListener(HandleDisableEvent);
        _player.GetEntityCompo<Health>().OnHit.AddListener(HandleFillEvent);
    }

    protected override void HandleDisableEvent()
    {
    }

    protected override void HandleFillEvent(float damage)
    {
        float fillAmount = _player.GetEntityCompo<Health>().GetNormalizeHealth();
        _fillImage.DOFillAmount(fillAmount, 0.3f);
    }
}
