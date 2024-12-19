using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private Image fade;
    void Start()
    {
        fade.DOFade(0,0.7f);
        print("123");
    }
        
    
}
