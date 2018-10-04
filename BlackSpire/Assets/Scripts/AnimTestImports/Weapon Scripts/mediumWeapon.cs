using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mediumWeapon : MonoBehaviour {

    public _equipped isEquipped;
    public bool activeMediumWeapon;

    void Start()
    {
        isEquipped = GetComponentInChildren<_equipped>();
        activeMediumWeapon = true;
    }


    void Update()
    {
        if (!activeMediumWeapon)
        {
            isEquipped.equipped = false;
        }
        else if (activeMediumWeapon)
        {
            isEquipped.equipped = true;
        }
    }
}
