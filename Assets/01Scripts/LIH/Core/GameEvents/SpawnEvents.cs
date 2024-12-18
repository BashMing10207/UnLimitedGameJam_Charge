using UnityEngine;

public static class SpawnEvents
{
    public static BulletCreate BulletCreate = new BulletCreate();
}

public class BulletCreate : GameEvent
{
    public PoolType _bulletType;
    public Vector2 position;
    public Vector2 dir;
    public float power;
}
