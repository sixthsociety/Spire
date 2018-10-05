using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : LightWeapon {

    private void Start()
    {
        currentAmmo = clipSize;
    }

    public override void DoShoot()
    {
        base.DoShoot();
        Debug.Log("BANG");
    }
}
