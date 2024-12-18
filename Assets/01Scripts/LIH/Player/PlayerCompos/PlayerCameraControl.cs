using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCameraControl : MonoBehaviour, IPlayerCompo
{
    private Player _player;
    [SerializeField] private GameEventChannelSO _cameraEventChannel;
    
    [Header("Charging Setting")]
    [SerializeField] private float _camDistanceMaxZoomValue = 10f;
    [SerializeField] private float _camDistanceMinValue = 4f;
    [SerializeField] private float _camDefaultDistacne = 5f;

    [Header("Fire Setting")]
    [SerializeField] private float _camShakeMaxValue = 50f;
    [SerializeField] private float _fireMinPower;
    [SerializeField] private float _fireMaxPower;
    public void Initialize(Player player)
    {
        _player = player;
    }

    public void ChargingCamSetting(float chargingValue)
    {
        CamChargingDistanceChange(chargingValue);
        CamChargingShake(chargingValue);
    }

    public void FireCamSetting(float power)
    {
        CamFireDistanceChange(power);
        CamFireShake(power);
    }

    private void CamChargingDistanceChange(float chargingValue)
    {
        var evt = CameraEvents.CamDistanceChangeEvent;
        float inverseLerp = Mathf.InverseLerp(0, _camDistanceMaxZoomValue, chargingValue);
        float distance = Mathf.Lerp(_camDefaultDistacne, _camDistanceMinValue, inverseLerp);
        
        evt.distance = distance;
        evt.speed = 1f;
        _cameraEventChannel.RaiseEvent(evt);
    }
    
    private void CamChargingShake(float chargingValue)
    {
        var evt = CameraEvents.CamShakeEvent;
        evt.intensity = Random.Range(0.1f, 0.2f);
        _cameraEventChannel.RaiseEvent(evt);
    }
    
    private void CamFireDistanceChange(float power)
    {
        var evt = CameraEvents.CamDistanceChangeEvent;
        evt.distance = _camDefaultDistacne;
        evt.speed = 2f;
        _cameraEventChannel.RaiseEvent(evt);
    }
    
    private void CamFireShake(float power)
    {
        var evt = CameraEvents.CamShakeEvent;
        
        float inverseLerp = Mathf.InverseLerp(0, _camShakeMaxValue, power);
        float intensity = Mathf.Lerp(_fireMinPower, _fireMaxPower, inverseLerp);

        evt.intensity = intensity;
        _cameraEventChannel.RaiseEvent(evt);
    }
}
