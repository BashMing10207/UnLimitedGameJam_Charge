using System;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO _cameraChannel;
    [SerializeField] private float _distanceChangeRate = 1.5f, _distanceThreshold = 0.25f, _shakeThreshold = 0.1f;
    [SerializeField] private CinemachineImpulseSource _impulseSource;

    private bool _isChangeComplete;

    private CinemachineCamera _vCam;
    private CinemachinePositionComposer _composer;

    private float _targetDistance;
    private float _camDistanceSpeed;
    private float _defaultYoffset;
    
    public float DefaultDistance { get; private set; }

    private void Awake()
    {
        _vCam = GetComponent<CinemachineCamera>();
        _composer = GetComponent<CinemachinePositionComposer>();
        _defaultYoffset = _composer.TargetOffset.y;
        
        _cameraChannel.AddListener<CamDistanceChange>(HandleCamDistanceChange);
        _cameraChannel.AddListener<CamShake>(HandleCamShake);
        _cameraChannel.AddListener<CamDistanceReset>(HandleCamDistanceReset);
        _cameraChannel.AddListener<CamOffsetChange>(HandleCamOffset);
        
        _targetDistance = _vCam.Lens.OrthographicSize;
        DefaultDistance = _vCam.Lens.OrthographicSize;
    }
    
    private void OnDestroy()
    {
        _cameraChannel.RemoveListener<CamDistanceChange>(HandleCamDistanceChange);
        _cameraChannel.RemoveListener<CamShake>(HandleCamShake);
        _cameraChannel.RemoveListener<CamDistanceReset>(HandleCamDistanceReset);
        _cameraChannel.RemoveListener<CamOffsetChange>(HandleCamOffset);
    }

    private void HandleCamOffset(CamOffsetChange evt)
    {
        Vector2 dir = evt.targetPos - evt.postion;
        
        float distance = Vector2.Distance(evt.targetPos, evt.postion);
        if (distance >= evt.radius)
        {
            dir.Normalize();
        }
        
        _composer.TargetOffset = new Vector3(Mathf.Abs(dir.x), dir.y + _defaultYoffset,0);
    }

    private void HandleCamShake(CamShake evt)
    {
        _impulseSource.DefaultVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        _impulseSource.GenerateImpulse(evt.intensity * _shakeThreshold);
    }

    private void HandleCamDistanceChange(CamDistanceChange evt)
    {
        _targetDistance = evt.distance;
        _isChangeComplete = false;
        _camDistanceSpeed = evt.speed;
    }
    
    private void HandleCamDistanceReset(CamDistanceReset evt)
    {
        _targetDistance = DefaultDistance;
        _isChangeComplete = false;
        _camDistanceSpeed = evt.speed;
    }

    private void Update()
    {
        UpdateCameraDistance();
    }

    private void UpdateCameraDistance()
    {
        if (_isChangeComplete)
            return;

        float currentDistance = _vCam.Lens.OrthographicSize;
        if (Mathf.Abs(currentDistance - _targetDistance) < _distanceThreshold)
        {
            _vCam.Lens.OrthographicSize = _targetDistance;
            _isChangeComplete = true;
        }
        else
        {
            _vCam.Lens.OrthographicSize = Mathf.Lerp(
                _vCam.Lens.OrthographicSize,
                _targetDistance,
                _distanceChangeRate * Time.deltaTime * _camDistanceSpeed);
        }
    }
}
