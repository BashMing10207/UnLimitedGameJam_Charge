using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerCameraControl : MonoBehaviour, IPlayerCompo
{
    private Player _player;
    [SerializeField] private GameEventChannelSO _cameraEventChannel;
    
    [Header("Charging Setting")]
    [SerializeField] private float _camDistanceMaxZoomValue = 10f;
    [SerializeField] private float _camDistanceMinValue = 4f;

    [Header("Fire Setting")]
    [SerializeField] private float _camShakeMaxValue = 50f;
    [SerializeField] private float _fireMinPower;
    [SerializeField] private float _fireMaxPower;

    private float _camDefaultDistance;
    
    public void Initialize(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        _camDefaultDistance = FindAnyObjectByType<PlayerCamera>().DefaultDistance;
    }

    public void ChargingCamSetting(float chargingTime, float chargingValue)
    {
        CamChargingDistanceChange(chargingTime,chargingValue);
        CamChargingShake(chargingTime,chargingValue);
    }

    public void FireCamSetting(float power)
    {
        CamFireDistanceChange(power);
        CamFireShake(power);
    }

    public void CamReset()
    {
        var evt = CameraEvents.CamShakeDistanceResetEvent;
        _cameraEventChannel.RaiseEvent(evt);
    }

    private void CamChargingDistanceChange(float chargingTime, float chargingValue)
    {
        var evt = CameraEvents.CamDistanceChangeEvent;
        float inverseLerp = Mathf.InverseLerp(0, _camDistanceMaxZoomValue, chargingValue);
        float distance = Mathf.Lerp(_camDefaultDistance, _camDistanceMinValue, inverseLerp);
        
        evt.distance = distance;
        evt.speed = 2f;
        _cameraEventChannel.RaiseEvent(evt);
    }
    
    private void CamChargingShake(float chargingTime, float chargingValue)
    {
        var evt = CameraEvents.CamShakeEvent;
        evt.intensity = Random.Range(0.1f, 0.2f);
        _cameraEventChannel.RaiseEvent(evt);
    }
    
    private void CamFireDistanceChange(float power)
    {
        var evt = CameraEvents.CamShakeDistanceResetEvent;
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
