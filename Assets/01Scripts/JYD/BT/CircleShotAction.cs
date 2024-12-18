using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CircleShot", story: "CircleShot [bulletCount]", category: "Action", id: "01eee6f622f063ae1daf591d467547bd")]
public partial class CircleShotAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> DownSpeed;
    
    [SerializeReference] public BlackboardVariable<int> BulletCount;
    [SerializeReference] public BlackboardVariable<GolemBoss> Golem;
    
    [SerializeReference] public BlackboardVariable<bool> IsLeft;
    
    protected override Status OnStart()
    {
        if (IsLeft.Value)
        {
            Golem.Value.TakeDownLeftAndCircleShot(BulletCount.Value,Speed.Value , DownSpeed.Value);
        }
        else
        {
            Golem.Value.TakeDownRightAndCircleShot(BulletCount.Value,Speed.Value , DownSpeed.Value);
        }
        
        return Status.Success;
    }
}

