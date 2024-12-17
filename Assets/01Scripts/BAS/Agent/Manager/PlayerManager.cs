using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : AgentManager
{
    
    [SerializeField]
    private PlayerInputSO _playerInput;

    public Vector2 PostMousePos { get; private set; } = new Vector2(0, 0);
    public bool IsHolding { get; private set; } = false;

    public float Upward { get; private set; } = 0;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void FixedUpdate()
    {
        MoveUnit(_playerInput.MoveDir);
    }

    private void Update()
    {
        for (int i = 0; i < Units.Count; i++)
        {
            if (i == SelectedUnitIdx)
            {
                Units[i].SelectedUpdate();
            }
            else
            {
                Units[i].DisSelectedUpdate();
            }
        }
    }

    protected void Init()
    {
        _playerInput.UnitSwapEvent += SwapNextUnit;
        _playerInput.OnClickEnter += HoldStart;
        _playerInput.OnClickEnter2 += AltFire;
    }

    private void GetAction(ActSO act)
    {
        ActionExcutor?.Invoke(act);
    }

    private void SwapNextUnit(int idx)
    {
        SwapUnit((SelectedUnitIdx + idx) % Units.Count);
    }
    private void SwapUnit(int idx)
    {
        Units[idx].SelectVisual(false);
        SelectedUnitIdx = idx;
        Units[idx].SelectVisual(true);
    }

    private void HoldStart()
    {

    }

    private void AltFire()
    {

    }

    private void MoveUnit(Vector2 dir)
    {
        float speed = SelectedUnit.GetCompo<AgentStat>().GetStat("Speed").Value;
        SelectedUnit.GetCompo<AgentMovment>().Move(dir * speed / 1.5f, speed);
    }
}

