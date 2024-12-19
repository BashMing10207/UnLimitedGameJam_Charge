using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO _spawnChannel;
    [SerializeField] private PoolManagerSO _poolManager;
    
    private void Awake()
    {
        _spawnChannel.AddListener<BulletCreate>(HandleBulletSpawn);
        _spawnChannel.AddListener<SmokeParticleCreate>(HandleSmokeParticleSpawn);
    }

    private void OnDestroy()
    {
        _spawnChannel.RemoveListener<BulletCreate>(HandleBulletSpawn);
        _spawnChannel.RemoveListener<SmokeParticleCreate>(HandleSmokeParticleSpawn);
    }

    private void HandleBulletSpawn(BulletCreate evt)
    {
        Bullet bullet = _poolManager.Pop(evt._bulletType) as Bullet;
        bullet.transform.position = evt.position;
        bullet.Shoot(evt.dir, evt.damage);
    }

    private void HandleSmokeParticleSpawn(SmokeParticleCreate evt)
    {
        SmokeParticle smoke = _poolManager.Pop(evt.poolType) as SmokeParticle;
        smoke.transform.position = evt.position;
        smoke.PlayParticle();
    }
}
