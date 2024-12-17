using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO _soundChannel;
    [SerializeField] private PoolManagerSO _poolManager;
    [SerializeField] private PoolType _soundPlyerType;

    private SoundPlayer _currentBGMPlayer = null;

    private void Awake()
    {
        _soundChannel.AddListener<PlaySFXEvent>(HandlePlaySFXEvent);
        _soundChannel.AddListener<PlayBGMEvent>(HandlePlayBGMEvent);
        _soundChannel.AddListener<StopBGMEvent>(HandleStopBGMEvent);
    }

    private void HandleStopBGMEvent(StopBGMEvent evt)
    {
        _currentBGMPlayer?.StopAndGotoPool(true); //fade아웃 시켜서 보내고
        _currentBGMPlayer = null;
    }

    private void HandlePlayBGMEvent(PlayBGMEvent evt)
    {
        _currentBGMPlayer?.StopAndGotoPool(true); //fade아웃 시켜서 보내고
        _currentBGMPlayer = _poolManager.Pop(_soundPlyerType) as SoundPlayer;
        _currentBGMPlayer.PlaySound(evt.clipData);
    }

    private void HandlePlaySFXEvent(PlaySFXEvent evt)
    {
        SoundPlayer player = _poolManager.Pop(_soundPlyerType) as SoundPlayer;
        player.transform.position = evt.position;
        player.PlaySound(evt.clipData);
    }

    private void OnDestroy()
    {
        _soundChannel.RemoveListener<PlaySFXEvent>(HandlePlaySFXEvent);
        _soundChannel.RemoveListener<PlayBGMEvent>(HandlePlayBGMEvent);
        _soundChannel.RemoveListener<StopBGMEvent>(HandleStopBGMEvent);
    }
}
