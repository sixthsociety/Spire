using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightWeapon : MonoBehaviour {

    public _equipped isEquipped;
    public bool activeLightWeapon;

    void Start()
    {
        isEquipped = GetComponentInChildren<_equipped>();
        activeLightWeapon = false;
    }


    void Update()
    {
        if (!activeLightWeapon)
        {
            isEquipped.equipped = false;
        }
        else if (activeLightWeapon)
        {
            isEquipped.equipped = true;
        }
    }
}
