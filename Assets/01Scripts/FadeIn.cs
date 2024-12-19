using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    public void SceneMove(string name)
    {
        _image.DOFade(1, 0.3f).OnComplete(() => SceneManager.LoadScene(name));
    }
}
