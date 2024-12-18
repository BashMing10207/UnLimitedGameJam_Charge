using UnityEngine;

public class EnemyFSM : MonoBehaviour,IEntityComponent
{
    private Enemy _enemy;

    private EnemyStateSO _currentState;

    public void Initialize(Entity entity)
    {
        _enemy = entity as Enemy;
    }

    private void Update()
    {
        
    }

}
