using System;
using System.Collections;
using UnityEngine;

public class BuffManager : MonoBehaviour, IGetCompoable
{
    public Action EffectTickUpdate;
    Agent _agent;

    public void Initialize(GetCompoParent entity)
    {
        _agent = entity as Agent;
    }
    IEnumerator EffectTick()
    {
        while(true)
        {
            EffectTickUpdate?.Invoke();
            yield return new WaitForSeconds(1);
        }
    }
}
