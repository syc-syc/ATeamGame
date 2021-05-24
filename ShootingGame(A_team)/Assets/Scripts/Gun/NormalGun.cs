using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : Gun
{

    public override void Shoot()
    {
        base.Shoot();
    }
    public override void GenerateBullet()
    {   
        Projectile newProjectile = bulletPool.GetInstance();
        newProjectile.onBulletDestory.AddListener(returnHitBulletToPool);
        newProjectile.transform.position = muzzle.position;
        newProjectile.transform.rotation = muzzle.rotation;
        newProjectile.gameObject.transform.SetParent(poolPositon);
        newProjectile.SetSpeed(muzzleVelocity);
        returnBullet(newProjectile);
    }
}
