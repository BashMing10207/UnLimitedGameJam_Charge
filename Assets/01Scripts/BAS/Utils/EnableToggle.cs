using UnityEngine;

public class EnableToggle : MonoBehaviour
{
    public GameObject aa;
    private void Start()
    {
        Invoke("DIs", 1f);
    }
   void DIs()
    {
        gameObject.SetActive(false);
    }
    public void EnableToggler()
    {
        aa.SetActive(!aa.activeInHierarchy);
    }
}
