using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {
    //Variables
    public heavyWeapon HA;
    public mediumWeapon MA;
    public lightWeapon LA;
    private float holdTime = 2.0f;
    private float startTime = 0f;
    public bool lightWeaponSwap = false;

void Start () {
        HA = GetComponentInChildren<heavyWeapon>();
        LA = GetComponentInChildren<lightWeapon>();
        MA = GetComponentInChildren<mediumWeapon>();
    }
	
	
	void FixedUpdate () {
        EquipCheck();
	}


    void EquipCheck()
    {
        //Heavy to Medium
        if ( Input.GetButtonDown("YButton") && HA.activeHeavyWeapon == true)
        {
            MA.activeMediumWeapon = true;
            HA.activeHeavyWeapon = false;
            LA.activeLightWeapon = false;
        }

        //Medium to Light
        else if (Input.GetButton("YButton") && MA.activeMediumWeapon == true)
        {

        }

        //Medium to Heavy
        else if (Input.GetButtonDown("YButton") && MA.activeMediumWeapon == true)
        {
            HA.activeHeavyWeapon = true;
            MA.activeMediumWeapon = false;
            LA.activeLightWeapon = false;
        }

        //Light to Medium
        else if (Input.GetButtonDown("YButton") && LA.activeLightWeapon == true)
        {
            MA.activeMediumWeapon = true;
            LA.activeLightWeapon = false;
            HA.activeHeavyWeapon = false;
        }

    }
}
