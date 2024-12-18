using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO _spawnChannel;
    [SerializeField] private PoolManagerSO _poolManager;

    private void Awake()
    {
        _spawnChannel.AddListener<BulletCreate>(HandleBulletSpawn);
    }

    private void OnDestroy()
    {
        _spawnChannel.RemoveListener<BulletCreate>(HandleBulletSpawn);
    }

    private void HandleBulletSpawn(BulletCreate evt)
    {
        Bullet bullet = _poolManager.Pop(evt._bulletType) as Bullet;
        bullet.transform.position = evt.position;
        bullet.Shoot(evt.dir, evt.power);
    }
}
