using UnityEngine;

public class TrailRendererSizeHandle : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer _trail;
    public void SetWidth(float width)
    {
        _trail.widthMultiplier = width;
    }
}
