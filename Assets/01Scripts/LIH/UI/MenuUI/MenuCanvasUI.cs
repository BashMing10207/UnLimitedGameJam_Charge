using System;
using DG.Tweening;
using UnityEngine;

public class MenuCanvasUI : MonoBehaviour
{
    public enum UIWindowStatus
    {
        Closed, Closing, Opened, Opening
    }

    [SerializeField] private GameEventChannelSO _uiEventChannel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private PlayerInputSO _playerInput;
    [SerializeField] private OptionUI _optionUI;

    private UIWindowStatus _windowStatus = UIWindowStatus.Closed;

    private void Awake()
    {
        _uiEventChannel.AddListener<OpenMenuEvent>(HandleOpenMenu);
    }

    private void OnDestroy()
    {
        _uiEventChannel.RemoveListener<OpenMenuEvent>(HandleOpenMenu);
    }

    private void HandleOpenMenu(OpenMenuEvent evt)
    {
        if (_windowStatus == UIWindowStatus.Closing || _windowStatus == UIWindowStatus.Opening)
            return; //진행중이라면 받지 않는다.

        if (_windowStatus == UIWindowStatus.Opened) //열려있다면 닫아야 한다.
        {
            CloseWindow();
        }
        else if(_windowStatus == UIWindowStatus.Closed) // 닫혀있다면 열어야 한다.
        {
            OpenWindow();
        }
    }

    private void OpenWindow(float duration = 0.3f)
    {
        Debug.Log("afaf");
        _windowStatus = UIWindowStatus.Opening;
        Time.timeScale = 0;
        _playerInput.SetPlayerInput(false);
        SetWindow(true, () => _windowStatus = UIWindowStatus.Opened, duration);
    }

    private void SetWindow(bool isOpen, Action callbackAction, float duration)
    {
        float alpha = isOpen ? 1f : 0f;
        _canvasGroup.DOFade(alpha, duration).SetUpdate(true).OnComplete(() => callbackAction?.Invoke());
        _canvasGroup.blocksRaycasts = isOpen;
        _canvasGroup.interactable = isOpen;
    }

    private void CloseWindow(float duration = 0.3f)
    {
        _windowStatus = UIWindowStatus.Closing;
        _playerInput.SetPlayerInput(true);
        SetWindow(false, () =>
        {
            _windowStatus = UIWindowStatus.Closed;
            Time.timeScale = 1f;
        }, duration);
    }
    }
