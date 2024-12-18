using UnityEngine;

public class CameraImpuseFeedback : Feedback
{
    [SerializeField] private GameEventChannelSO _camEventChannelSo;
    [SerializeField] private float _intensity;
    
    public override void CreateFeedback()
    {
        var evt = CameraEvents.CamShakeEvent;
        evt.intensity = _intensity;
        _camEventChannelSo.RaiseEvent(evt);
    }

    public override void FinishFeedback()
    {
    }
}
