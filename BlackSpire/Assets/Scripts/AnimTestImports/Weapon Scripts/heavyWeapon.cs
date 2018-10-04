using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heavyWeapon : MonoBehaviour {

    public _equipped isEquipped;
    public bool activeHeavyWeapon;

	void Start () {
        isEquipped = GetComponentInChildren<_equipped>();
        activeHeavyWeapon = false;
	}
	

	void Update () {
		if (!activeHeavyWeapon)
        {
            isEquipped.equipped = false;
        }
        else if (activeHeavyWeapon)
        {
            isEquipped.equipped = true;
        }
	}
}
