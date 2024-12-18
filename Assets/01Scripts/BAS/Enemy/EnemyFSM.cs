using UnityEngine;

public class EnemyFSM : MonoBehaviour,IEntityComponent
{
    private Enemy _enemy;

    [SerializeField]
    private EnemyStateSO _currentState;

    public void Initialize(Entity entity)
    {
        _enemy = entity as Enemy;
    }

    public void SetState(EnemyStateSO state)
    {
        _currentState.OnExit();
        _currentState = state;
        _currentState.OnEnter(_enemy);
    }

    private void Update()
    {
        _currentState.Update();
    }

}
