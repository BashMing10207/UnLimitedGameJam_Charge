using UnityEngine;

public class DefaultWeapon : Weapon
{
    public override void Charging(float chargingTime, float chargingSpeed)
    {
        base.Charging(chargingTime,chargingSpeed);
    }

    public override void Fire(float power)
    {
        Bullet bullet = Instantiate(_bullet);
        bullet.transform.position = _fireTrm.position;
        bullet.Shoot(_player.LookDir(), power);
        base.Fire(power);
    }
}
