using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO _spawnChannel;
    [SerializeField] private PoolManagerSO _poolManager;
    
    private void Awake()
    {
        _spawnChannel.AddListener<BulletCreate>(HandleBulletSpawn);
        _spawnChannel.AddListener<SmokeParticleCreate>(HandleSmokeParticleSpawn);
        _spawnChannel.AddListener<RockCreate>(HandleRockSpawn);
        _spawnChannel.AddListener<ExplosionCreate>(HandleExplosionSpawn);
    }

    private void OnDestroy()
    {
        _spawnChannel.RemoveListener<BulletCreate>(HandleBulletSpawn);
        _spawnChannel.RemoveListener<SmokeParticleCreate>(HandleSmokeParticleSpawn);
        _spawnChannel.RemoveListener<RockCreate>(HandleRockSpawn);
        _spawnChannel.RemoveListener<ExplosionCreate>(HandleExplosionSpawn);
    }

    private void HandleBulletSpawn(BulletCreate evt)
    {
        Bullet bullet = _poolManager.Pop(evt._bulletType) as Bullet;
        bullet.transform.position = evt.position;
        //bullet.transform.localScale = Vector3.one * (0.5f+evt.size/2); 
        bullet.Shoot(evt.dir, evt.damage, evt.speed);

        if (bullet is EnemyBullet enemyBullet)
        {
            enemyBullet.PlaySound();
        }
    }

    private void HandleSmokeParticleSpawn(SmokeParticleCreate evt)
    {
        SmokeParticle smoke = _poolManager.Pop(evt.poolType) as SmokeParticle;
        smoke.transform.position = evt.position;
        smoke.PlayParticle();
    }

    private void HandleRockSpawn(RockCreate evt)
    {
        Rock rock = _poolManager.Pop(evt.poolType) as Rock;
        rock.transform.position = evt.position;
        rock.SetDirection(evt.direction , evt.fallTime);
    }

    private void HandleExplosionSpawn(ExplosionCreate evt)
    {
        Explosion ex = _poolManager.Pop(evt.poolType) as Explosion;
        ex.transform.position = evt.position;
        ex.PlayParticle();
        
    }
}
