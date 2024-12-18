using UnityEngine;

public class DefaultWeapon : Weapon
{
    public override void Charging(float chargingSpeed)
    {
        base.Charging(chargingSpeed);
    }

    public override void Fire()
    {
        PlayerBullet playerBullet = Instantiate(_bullet);
        playerBullet.transform.position = _fireTrm.position;
        playerBullet.Shoot(_player.LookDir(), _currentCharging);
        base.Fire();
    }
}
