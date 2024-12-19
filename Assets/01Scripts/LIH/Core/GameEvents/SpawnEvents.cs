using UnityEngine;

public static class SpawnEvents
{
    public static BulletCreate BulletCreate = new BulletCreate();
    public static SmokeParticleCreate SmokeParticleCreate = new SmokeParticleCreate();
}

public class BulletCreate : GameEvent
{
    public PoolType _bulletType;
    public Vector2 position;
    public Vector2 dir;
    public float damage;
}

public class SmokeParticleCreate : GameEvent
{
    public PoolType poolType ;
    public Vector2 position;
    public Vector2 dir;
}
