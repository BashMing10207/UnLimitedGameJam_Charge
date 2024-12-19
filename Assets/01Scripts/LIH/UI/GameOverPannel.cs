using System;
using DG.Tweening;
using UnityEngine;

public class GameOverPannel : MonoBehaviour
{
    [SerializeField] private float _alphaTime;

    private CanvasGroup _group;

    private void Awake()
    {
        _group = GetComponent<CanvasGroup>();
    }
    
    public void OpenWindow()
    {
        gameObject.SetActive(true);
        _group.DOFade(1, _alphaTime).OnComplete(() => Time.timeScale = 0f);
    }

    public void ReStart()
    {
        Time.timeScale = 1f;
    }

    public void Lobby()
    {
        Time.timeScale = 1f;
    }
}
