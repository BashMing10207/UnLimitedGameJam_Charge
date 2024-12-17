using System;
using UnityEngine;

public class ActCommander : MonoBehaviour,IGetCompoable
{
    private AgentManager _manager;

    public float ActionPoint = 10f;

    public Action ActFail;

    public void Initialize(GetCompoParent entity)
    {
        _manager = entity as AgentManager;
        _manager.ActionExcutor += TrySkill;

    }

    protected void TrySkill(ActSO act)
    {
        if (act.IsActive)
        {
            ActFail?.Invoke();
            return;
        }

        _manager.SelectedUnit.GetCompo<AgentActCommander>().ExecuteAct(act);
    }

}
