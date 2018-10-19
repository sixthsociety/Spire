using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MediumWeapon {

    private void Start()
    {
        currentAmmo = clipSize;
    }

    public override void DoShoot()
    {
        base.DoShoot();

        // assault rifle shooting pattern

        RaycastHit hit;

        if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, range))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
