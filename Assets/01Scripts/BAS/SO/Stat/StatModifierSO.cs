using UnityEngine;

[CreateAssetMenu(fileName = "StatModifierSO", menuName = "SO/Stat/StatModifierSO")]
public class StatModifierSO : ScriptableObject
{
    public Texture2D Icon;

    public string Name = "";

    public int MaxStack = 5;

    public int RemainingTurn = 2;

    public StatSO TargetStat;
    public bool IsMultiply = false; // true: multiply false: plus

    public float ModifierValue = 0.05f;
}
