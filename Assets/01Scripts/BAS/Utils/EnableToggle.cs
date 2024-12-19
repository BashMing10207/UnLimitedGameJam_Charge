using UnityEngine;

public class EnableToggle : MonoBehaviour
{
    public GameObject aa;
    public void EnableToggler()
    {
        aa.SetActive(!aa.activeInHierarchy);
    }
}
