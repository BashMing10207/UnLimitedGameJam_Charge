using UnityEngine;

[CreateAssetMenu(fileName = "StatActSO", menuName = "SO/Act/StatActSO")]
public class StatChangeSO : ActSO
{
    [SerializeField]
    protected StatSO _getImpactStat2This;//이 효과에 영향을 주는 스텟
    [SerializeField]
    protected StatModifierSO _moidfier;

    [SerializeField] 
    protected int _keepingTurn = 10;
    public override void RunAct(ref Agent agent)
    {
        Debug.Log("a");
        float strength = 1;
        StatModifierSO moidfier = _moidfier;


        if (_getImpactStat2This != null)
        {
            StatSO stat = agent.GetCompo<AgentStat>().GetStat(_getImpactStat2This.name);
            strength = stat.Value; //지능 등의 수치를 적용시킬 수 있다 ㅎㅎ
        }

        moidfier.ModifierValue *= strength;

        agent.GetCompo<AgentStat>().GetStat(_moidfier.TargetStat.StatName).TryAddTemponaryModifiler(moidfier);
    }
}
