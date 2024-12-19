using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPannel : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO _gameEventChannelSo;
    [SerializeField] private SoundSO _so;
    
    [SerializeField] private float _alphaTime;
    [SerializeField] private Animator animator;
    
    private CanvasGroup _group;
    

    private void Awake()
    {
        _group = GetComponent<CanvasGroup>();
    }
    
    public void OpenWindow()
    {
        gameObject.SetActive(true);
        _group.DOFade(1, _alphaTime).OnComplete(() =>
        {
            Time.timeScale = 0f;
            animator.SetTrigger("Die");
        });
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
