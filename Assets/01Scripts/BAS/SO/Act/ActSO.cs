using UnityEngine;

public enum ActType
    {
        Projecile,
        Passive,
        Active
    }
//[CreateAssetMenu(fileName="SO/Act")]
public abstract class ActSO : ScriptableObject
{
    public bool IsActive = true;

    public float CoolDown = 1f;

    public string ActName = "";
    public string Description = "";


    public abstract void RunAct(ref Agent agent);
}
