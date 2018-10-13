using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWeapon : Weapon {

    private void Start()
    {
        SetAmmoType(1);
    }

    public override void DoShoot()
    {
        base.DoShoot();

        RaycastHit hit;

        if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, range))
        {
            //if (hit.collider.GetComponent<Enemy>())
            {
                //hit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
