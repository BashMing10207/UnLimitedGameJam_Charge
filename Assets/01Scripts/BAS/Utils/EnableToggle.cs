using UnityEngine;

public class EnableToggle : MonoBehaviour
{
    public void EnableToggler()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
