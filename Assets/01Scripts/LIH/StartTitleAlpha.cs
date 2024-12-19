using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartTitleAlpha : MonoBehaviour
{
    [SerializeField] private float _alphaValue = 0.6f;
    [SerializeField] private float _time = 3f;
    [SerializeField] private Image _image;

    [SerializeField] private GameEventChannelSO _eventChannelSo;
    [SerializeField] private SoundSO So;

    private void Awake()
    {
        _image.DOFade(_alphaValue, _time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InCubic);
    }

    private void Start()
    {
        var evt = SoundEvents.PlayBGMEvent;
        evt.clipData = So;
        _eventChannelSo.RaiseEvent(evt);
    }
}
