using UnityEngine;

public abstract class EnemyStateSO : ScriptableObject
{
    public string StateName;

    public AnimStateSO AnimState;

    protected Entity _entity;

    public EnemyStateSO NextState;

    public abstract void Update();

    public abstract void OnEnter(Entity entity);

    public abstract void OnExit();

    public virtual void DoExit()
    {
        _entity.GetEntityCompo<EnemyFSM>().SetState(NextState);
    }
}
