using UnityEngine;

    public class BashUtils
    {
        public static Vector3 V2ToV3(Vector2 v)
        {
            return new Vector3(v.x, 0, v.y);
        }
        public static Quaternion LookDir2(Vector3 v)
        { 
            return Quaternion.LookRotation(v, Vector3.forward);
        }
    }
public class SetablePair<T, T2> //c#의 Pair와 Tuple은 readonly이기에 새로 만든
{
    public T First { get; set; }
    public T2 Second { get; set; }
    public SetablePair(T key, T2 val)
    {
        this.First = key;
        this.Second = val;
    }
}