using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyWeapon : Weapon {

    [SerializeField] protected Projectile bullet; // used by heavy weapons as a script that sits on the projectile they fire

    private void Start()
    {
        SetAmmoType(3);
    }
}
