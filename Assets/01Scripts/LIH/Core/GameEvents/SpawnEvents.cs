using UnityEngine;

public static class SpawnEvents
{
    public static BulletCreate BulletCreate = new BulletCreate();
    public static SmokeParticleCreate SmokeParticleCreate = new SmokeParticleCreate();
    public static RockCreate RockCreate = new RockCreate();
    public static ExplosionCreate ExplosionCreate = new ExplosionCreate();

}

public class BulletCreate : GameEvent
{
    public PoolType _bulletType;
    public Vector2 position;
    public Vector2 dir;
    public float damage;
    public float speed = 1f;
    public float size = 1f;
}

public class SmokeParticleCreate : GameEvent
{
    public PoolType poolType ;
    public Vector2 position;
}

public class RockCreate : GameEvent
{
    public PoolType poolType ;
    public Vector2 position;
    public Vector2 direction;
    public float fallTime;
}

public class ExplosionCreate : GameEvent
{
    public PoolType poolType ;
    public Vector2 position;
}