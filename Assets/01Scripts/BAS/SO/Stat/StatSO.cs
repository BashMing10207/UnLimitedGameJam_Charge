using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSO", menuName = "SO/Stat/StatSO")]
public class StatSO : ScriptableObject, ICloneable //SO를 생성할때마다 인스턴스 되게 하는거?
{
    public string StatName;
    public string Description;

    public float Value => _baseValue * GetModifierValue(); //람다식 :D

    [SerializeField]
    protected float _baseValue = 1;
    public List<SetablePair<StatModifierSO, int>> Modifiers = new List<SetablePair<StatModifierSO, int>>();
    public List<SetablePair<StatModifierSO, int>> TempModifilerAndRemain = new List<SetablePair<StatModifierSO, int>>();
    public virtual object Clone()
    {
        return Instantiate(this); //(아마도)SO를 만들 때 호출하여 기존 값을 복제하는 역할. https://learn.microsoft.com/en-us/dotnet/api/system.icloneable?view=net-9.0 <- (dd)
    }

    public void SetBaseValue(float value)
    {
        _baseValue = value; 
    }

    protected virtual float GetModifierValue()
    {
        float a = 0;
        List<float> b = new List<float>();

        if (Modifiers.Count > 0)
        {
            foreach (var modifier in Modifiers)
            {
                if (modifier.First.IsMultiply)
                {
                    b.Add(modifier.Second);
                }
                else
                {
                    a += (modifier.Second);
                }
            }
        }

        a += 1; //((1 + add)*multi) * baseStat
        if (Modifiers.Count > 0)
        {
            foreach (var c in b)
            {
                a *= c;
            }
        }

            return a;
    }
    public void TryAddModifier(StatModifierSO mod)
    {
        foreach (var modifier in Modifiers)
        {
            if (modifier.First == mod)
            {
                modifier.Second = Mathf.Min(modifier.Second+1,mod.MaxStack);
                return;
            }
        }

        Modifiers.Add(new SetablePair<StatModifierSO, int>(mod , 1));
    }

    public void TryAddTemponaryModifiler(StatModifierSO mod)
    {
        TryAddModifier(mod);
        TempModifilerAndRemain.Add(new SetablePair<StatModifierSO, int>(mod, mod.RemainingTurn));
    }

    public void TryRemoveModifier(StatModifierSO mod)
    {
        foreach (var modifier in Modifiers)
        {
            if (modifier.First == mod)
            {
                modifier.Second = Mathf.Max(modifier.Second-1,0);
                return;
            }
        }

        Modifiers.Add(new SetablePair<StatModifierSO, int>(mod, 0));
    }
}
