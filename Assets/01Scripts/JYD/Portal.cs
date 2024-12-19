using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private TextMeshProUGUI text;
    
    [Space]
    [SerializeField] private bool isEffect;
    [SerializeField] private SpriteRenderer portal;
    private readonly int _value = Shader.PropertyToID("_Value");

    [SerializeField] private Image fade;
    
    private SpriteRenderer frame => GetComponent<SpriteRenderer>();
    
    private void OnEnable()
    {
        if (isEffect)
        {
            Material portalMaterial = frame.material;
            float initialValue = portalMaterial.GetFloat(_value);
            
            DOVirtual.Float(initialValue, 1.1f, 3, value =>
            {
                portalMaterial.SetFloat(_value, value);
            }).OnComplete(() =>
            {
                portal.DOFade(1, 1);
            });
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        text.text = "F";
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            fade.DOFade(1,0.7f).OnComplete(() =>
            {
                SceneManager.LoadScene(nextSceneName);
            });
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        text.text = " ";
    }
            
}
