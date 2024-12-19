using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private TextMeshProUGUI text;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        text.text = "F";
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        text.text = " ";
    }
            
}
