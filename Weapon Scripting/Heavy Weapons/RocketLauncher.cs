using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : HeavyWeapon {

    [SerializeField] protected ParticleSystem barrelExplosion; // the effect when player fires gun

    private void Start()
    {
        currentAmmo = clipSize;
    }

    public override void DoShoot()
    {
        base.DoShoot();

        Projectile lastRocket = Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
