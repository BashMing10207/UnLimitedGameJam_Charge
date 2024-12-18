using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private PlayerManagerSO _playerManagerSO;
    [field: SerializeField] public PlayerInputSO PlayerInput { get; set; }

    private Dictionary<Type, IPlayerCompo> _playerCompos;

    protected override void Awake()
    {
        base.Awake();
        _playerManagerSO.SetPlayer(this);
    }

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        
        _playerCompos = new Dictionary<Type, IPlayerCompo>();
        
        GetComponentsInChildren<IPlayerCompo>()
            .ToList()
            .ForEach(compo => _playerCompos.Add(compo.GetType(), compo));

        foreach (var compo in _playerCompos.Values)
        {
            compo.Initialize(this);
        }
    }

    public T GetPlayerCompo<T>() where T : IPlayerCompo
    {
        if(_playerCompos.TryGetValue(typeof(T), out IPlayerCompo compo))
        {
            return (T)compo;
        }
        return default;
    }

    public Vector2 LookDir() => (PlayerInput.MousePos - (Vector2)transform.position).normalized;
}
