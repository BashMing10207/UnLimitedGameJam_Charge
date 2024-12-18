using System;
using UnityEngine;

public class PlayerRender : MonoBehaviour, IPlayerCompo
{
    [SerializeField] private float _flipOffset;
    [field: SerializeField] public float FacingDirection { get; private set; } = 1;
    
    private Player _player;
    private PlayerInputSO _playerInput;

    private Vector2 _mousePos;

    public void Initialize(Player player)
    {
        _player = player;
        _playerInput = _player.PlayerInput;

        _playerInput.MouseMoveEvent += HandleFlipController;
    }

    private void OnDestroy()
    {
        _playerInput.MouseMoveEvent -= HandleFlipController;
    }

    private void Update()
    {
        if (transform.position.x + _flipOffset <= _mousePos.x && FacingDirection < 0)
            Flip();
        else if(transform.position.x - _flipOffset >= _mousePos.x && FacingDirection > 0)
            Flip();
    }

    private void HandleFlipController(Vector2 mousePos)
    {
        _mousePos = mousePos;
    }

    private void Flip()
    {
        FacingDirection *= -1;
        _player.transform.Rotate(0, 180f, 0);
    }
}
