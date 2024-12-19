using UnityEngine;

public class TrailRendererSizeHandle : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer _trail;

    [SerializeField] private float _maxWidth = 1.5f;
    [SerializeField] private float _maxWidthPower = 100f;
        
    public void SetWidth(float width)
    {
        float t = Mathf.InverseLerp(0, _maxWidthPower, width);
        float lerp = Mathf.Lerp(1f, _maxWidth, t);
        _trail.widthMultiplier = lerp;
    }
}
