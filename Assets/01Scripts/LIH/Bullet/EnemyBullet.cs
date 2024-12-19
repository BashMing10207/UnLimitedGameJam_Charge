using System;
using UnityEngine;

public class EnemyBullet : Bullet
{
    [SerializeField] private SoundSO fireAudio;
    [SerializeField] private GameEventChannelSO soundChannelSo;

    private void OnEnable()
    {
        var evt = SoundEvents.PlaySfxEvent;
        evt.clipData = fireAudio;
        
        soundChannelSo.RaiseEvent(evt);

    }
}
