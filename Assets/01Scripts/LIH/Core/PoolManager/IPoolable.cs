using UnityEngine;

public enum PoolType
{
    PlayerBullet,
    EnemyBullet,
    SoundPlayer,
    Particle
}

public interface IPoolable
{
    public PoolType Type { get; }
    public GameObject GameObject { get; }
    public void SetUpPool(Pool pool);
    public void ResetItem();
}
