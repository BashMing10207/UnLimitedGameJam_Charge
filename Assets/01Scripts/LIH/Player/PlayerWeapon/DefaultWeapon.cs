using UnityEngine;

public class DefaultWeapon : Weapon
{
    public override void Charging(float chargingTime, float chargingSpeed)
    {
        base.Charging(chargingTime,chargingSpeed);
    }

    public override void Fire(float power)
    {
        var evt = SpawnEvents.BulletCreate;
        evt._bulletType = PoolType.PlayerBullet;
        evt.damage = power;
        evt.dir = _player.LookDir();
        evt.position = _fireTrm.position;
        
        _spawnChannel.RaiseEvent(evt);
        base.Fire(power);
    }
}
