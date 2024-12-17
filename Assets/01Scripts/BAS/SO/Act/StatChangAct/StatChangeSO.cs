using UnityEngine;

[CreateAssetMenu(fileName = "StatActSO", menuName = "SO/Act/StatActSO")]
public class StatChangeSO : ActSO
{
    [SerializeField]
    protected StatSO _getImpactStat2This;//�� ȿ���� ������ �ִ� ����
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
            strength = stat.Value; //���� ���� ��ġ�� �����ų �� �ִ� ����
        }

        moidfier.ModifierValue *= strength;

        agent.GetCompo<AgentStat>().GetStat(_moidfier.TargetStat.StatName).TryAddTemponaryModifiler(moidfier);
    }
}
