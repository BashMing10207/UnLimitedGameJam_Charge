using System;
using UnityEngine;


public class AgentMovment : MonoBehaviour, IGetCompoable
{
    public Action<Vector2> OnMovement;
    private Rigidbody2D _rbCompo;

    private float _moveSpeed = 3f;
    private Vector2 _movement;
    private Agent _agent;

    private float _speedMultiplier;

    [SerializeField]
    private float _maxSpeed = 10;

    public void SetSpeedMultiplier(float value) => _speedMultiplier = value;

    public void Initialize(GetCompoParent entity)
    {
        _agent = (Agent)entity;
        _rbCompo = entity.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction, float maxSpeed)
    {
        OnMovement?.Invoke(_rbCompo.linearVelocity);
        _rbCompo.AddForce(direction * Mathf.Lerp(1, 0, ((Vector2)Vector3.Project(direction, _rbCompo.linearVelocity) + _rbCompo.linearVelocity).magnitude / maxSpeed), ForceMode2D.Impulse);
    }
    public void Move(Vector2 direction)
    {
        Move(direction, _maxSpeed);
    }
    public void AddForce(Vector3 dir)
    {
        _rbCompo.AddForce(dir, ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {

    }
}
