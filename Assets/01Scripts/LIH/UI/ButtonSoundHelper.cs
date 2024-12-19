using UnityEngine;

public class ButtonSoundHelper : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private GameEventChannelSO _soundChannelSo;
    [SerializeField] private SoundSO _buttonClick;

    public void PlaySound()
    {
        var evt2 = SoundEvents.PlaySfxEvent;
        evt2.clipData = _buttonClick;
        _soundChannelSo.RaiseEvent(evt2);
    }
}
