using UnityEngine;

public enum PoolType
{
    PlayerBullet,
    EnemyBullet,
    SoundPlayer,
    ImpactParticle,
    ExplosionParticle,
    Rock
    Particle,
    EnemyHomingBullet,
    EnemyFastBullet
}

public interface IPoolable
{
    public PoolType Type { get; }
    public GameObject GameObject { get; }
    public void SetUpPool(Pool pool);
    public void ResetItem();
}
