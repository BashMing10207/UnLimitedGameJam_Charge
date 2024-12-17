using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StatSO", menuName = "SO/Stat/Stat&EventSO")]
public class StatWithEventSO : StatSO
{
    public UnityEvent<float> ChangeValue;

    protected override float GetModifierValue()
    {
        float a = base.GetModifierValue();

        ChangeValue?.Invoke(a);

        return a;
    }
}
