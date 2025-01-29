using UnityEngine;


public class Portal : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private FadeIn fade;
    
    private readonly int _value = Shader.PropertyToID("_Value");

    private PlayerInputSO _playerInput;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((whatIsTarget & (1 << other.gameObject.layer)) != 0)
        {
            if (_playerInput == null)
            {
                _playerInput = other.GetComponent<Player>().PlayerInput;
            }
            
            _playerInput.InteractEvent += TransitionScene;
        }  
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if ((whatIsTarget & (1 << other.gameObject.layer)) != 0)
        {
            _playerInput.InteractEvent -= TransitionScene;
            _playerInput = null;
        }  
                
    }

    private void TransitionScene()
    {
        fade.FadeStart(nextSceneName);
    }
    
            
}
