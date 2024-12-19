using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BossHealthBar : HudBarUI
{
    [SerializeField] private Entity _boss;
    [SerializeField] private PlayerManagerSO _playerManagerSo;
    [SerializeField] private Image _predictionImage;
    
    private Player _player;
    
    private void Start()
    {
        _player = _playerManagerSo.Player;
        
        _boss.GetEntityCompo<Health>().OnDead.AddListener(HandleDisableEvent);
        _boss.GetEntityCompo<Health>().OnHit.AddListener(HandleFillEvent);
        //_player.GetPlayerCompo<PlayerWeaponController>().chargingEvent.AddListener(HandlePredictionEvent);
    }

    private void HandlePredictionEvent(float time, float value)
    {
        float fillAmount = _boss.GetEntityCompo<Health>().GetPredictionNormalizeHealth(value);
        Debug.Log(fillAmount);
        _predictionImage.DOFillAmount(1f- fillAmount, 0.3f);
    }

    protected override void HandleDisableEvent()
    {
        _canvasGroup.DOFade(0, 1);
    }

    protected override void HandleFillEvent(float damage)
    {
        float fillAmount = _boss.GetEntityCompo<Health>().GetNormalizeHealth();
        _fillImage.DOFillAmount(fillAmount, 0.3f);

        // float width = Mathf.Lerp(0, _predictionImage.rectTransform.sizeDelta.x, fillAmount);
        // _predictionImage.rectTransform.sizeDelta = new Vector2(width, _predictionImage.rectTransform.sizeDelta.y);
    }
}
