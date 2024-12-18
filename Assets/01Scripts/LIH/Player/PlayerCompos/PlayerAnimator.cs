using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IPlayerCompo
{
    private Player _player;
    private Animator _animator;

    private readonly int _moveHash = Animator.StringToHash("Move");
    private readonly int _idleHash = Animator.StringToHash("Idle");
    
    public void Initialize(Player player)
    {
        _player = player;
        _animator = GetComponent<Animator>();

        _player.PlayerInput.MovementEvent += HandleMoveAnim;
    }

    private void OnDestroy()
    {
        _player.PlayerInput.MovementEvent -= HandleMoveAnim;
    }

    private void HandleMoveAnim(bool isMove)
    {
        _animator.SetBool(_moveHash, isMove);
        _animator.SetBool(_idleHash, !isMove);
    }
}
