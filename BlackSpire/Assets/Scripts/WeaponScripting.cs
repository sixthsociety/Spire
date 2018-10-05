﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScripting : MonoBehaviour {

    public int selectedWeapon = 0;
    InputManager input;

	// Use this for initialization
	void Start () {
        SelectWeapon();
	}
	
	// Update is called once per frame
	void Update () {
		if (input.YButton)
        {
            selectedWeapon++;
        }
	}

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}