using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TakeDown", story: "TakeDown", category: "Action", id: "72cca1d57c1af06320e8c885db341dee")]
public partial class TakeDownAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> DownSpeed;
    
    [SerializeReference] public BlackboardVariable<bool> IsLeft;
    [SerializeReference] public BlackboardVariable<GolemBoss> Golem;
    
    protected override Status OnStart()
    {
        if(IsLeft.Value)
            Golem.Value.TakeDownLeft(Speed.Value , DownSpeed.Value);
        else
        {
            Golem.Value.TakeDownRight(Speed.Value , DownSpeed.Value);
        }
        
        return Status.Success;
    }
    


}

