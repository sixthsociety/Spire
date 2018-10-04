using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepController : MonoBehaviour {

    public enum WT
    {
        Pistol = 0, Rifle = 1, Minigun = 2, RocketLauncher = 3, Melee = 4, Laser = 5
    }

    public WT Type = WT.Rifle;

    InputManager inputManager;


	void Start () {
		
	}
	
	void Update () {
		
	}

    void SwitchWeapon()
    {
        
    }


}
