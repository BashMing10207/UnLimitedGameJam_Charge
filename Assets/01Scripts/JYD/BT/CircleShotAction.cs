using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CircleShot", story: "CircleShot [bulletCount]", category: "Action", id: "01eee6f622f063ae1daf591d467547bd")]
public partial class CircleShotAction : Action
{
    [SerializeReference] public BlackboardVariable<int> BulletCount;
    [SerializeReference] public BlackboardVariable<GolemBoss> Golem;
        
    protected override Status OnStart()
    {
        Golem.Value.CircleShot(BulletCount.Value);
        
        return Status.Success;
    }
}

