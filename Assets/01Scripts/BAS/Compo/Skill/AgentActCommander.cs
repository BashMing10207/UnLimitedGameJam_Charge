using System.Collections.Generic;
using UnityEngine;

public class AgentActCommander : MonoBehaviour, IGetCompoable
{
    private Agent _agent;
    public List<ActSO> ownActs = new List<ActSO>();

    public void Initialize(GetCompoParent entity)
    {
        _agent = entity as Agent;
    }

    public void ExecuteAct(ActSO act)
    {
        act.RunAct(ref _agent);
    }

}
