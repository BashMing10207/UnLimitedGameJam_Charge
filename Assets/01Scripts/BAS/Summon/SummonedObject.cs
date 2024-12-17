using UnityEngine;

public abstract class SummonedObject : MonoBehaviour //인터페이스면 추상화 안되니까 추상클래스로
{
    public abstract void Init(Vector3 a, Vector3 b, float stat);

}