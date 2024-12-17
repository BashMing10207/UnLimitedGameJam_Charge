using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "MoveActSO", menuName = "SO/Act/MoveActSO")]

public class MoveActSO : ActSO
{
    [SerializeField]
    private StatSO _targetstat;
    public override void RunAct(ref Agent agent)
    {
        if (_targetstat == null) return;
        StatSO stat = agent.GetCompo<AgentStat>().GetStat(_targetstat.StatName);
        if (stat == null) return;
        Vector2 direction = agent.transform.position - Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        agent.GetCompo<AgentMovment>().AddForce(direction);
        Debug.Log(direction);
    }
}
