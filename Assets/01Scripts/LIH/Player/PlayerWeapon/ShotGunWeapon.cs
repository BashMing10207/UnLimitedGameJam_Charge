using UnityEngine;

public class ShotGunWeapon : Weapon
{
    [SerializeField] private float _oneBulletPower;
    [SerializeField] private float _spreadValue;
    
    public override void Charging(float chargingTime, float chargingSpeed)
    {
        base.Charging(chargingTime,chargingSpeed);
    }

    public override void Fire(float power)
    {
        float bulletCount = power / _oneBulletPower;

        for (int i = 0; i < bulletCount; i++)
        {
            var evt = SpawnEvents.BulletCreate;
            evt._bulletType = PoolType.PlayerBullet;
            evt.damage = power / bulletCount;

            Vector2 dir = _player.LookDir();
            float x = Random.Range(-_spreadValue, _spreadValue);
            float y = Random.Range(-_spreadValue, _spreadValue);
            dir.x += x;
            dir.y += y;

            evt.speed = Random.Range(1f, 1.5f);
            evt.dir = dir.normalized;
            evt.position = _fireTrm.position;
            _spawnChannel.RaiseEvent(evt);
        }
        base.Fire(power);
    }
}
