﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    private Weapon defaultWeapon;
    [SerializeField] private LightWeapon lWeapon; // light weapon attached to the player
    [SerializeField] private MediumWeapon mWeapon; // medium weapon attached to the player
    [SerializeField] private HeavyWeapon hWeapon; // heavy weapon attached to the player

    private Weapon currentWeapon; // the active weapon
    private bool isAiming;

    private void Start()
    {
        lWeapon.gameObject.SetActive(false);
        mWeapon.gameObject.SetActive(false);
        hWeapon.gameObject.SetActive(false);

        defaultWeapon = mWeapon;
        EquipWeapon(defaultWeapon);
    }

    void EquipWeapon (Weapon toEquip) 
    {
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }
        currentWeapon = toEquip;
        currentWeapon.gameObject.SetActive(true);
        
    }

    void SwitchWeapon () 
    {
        if (currentWeapon.GetComponent<LightWeapon>())
        {
            EquipWeapon(mWeapon);
        }

        else if (currentWeapon.GetComponent<MediumWeapon>())
        {
            EquipWeapon(hWeapon);
        }

        else if (currentWeapon.GetComponent<HeavyWeapon>())
        {
            EquipWeapon(lWeapon);
        }
    }

    void Shoot () 
    {
        if(isAiming)
        currentWeapon.ShootTrigger();
    }

    void Reload () 
    {
        currentWeapon.ReloadTrigger();
    }

    public void PickUpAmmo (int _ammoAmount, int _ammoType) 
    {
        Debug.Log(_ammoType);

        if (_ammoType == 0)
        {
            lWeapon.AddAmmo(_ammoAmount);
        }

        if (_ammoType == 1)
        {
            mWeapon.AddAmmo(_ammoAmount);
        }

        if (_ammoType == 2)
        {
            hWeapon.AddAmmo(_ammoAmount);
        }
    }

    public bool GetAim () 
    {
        return isAiming;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") || Input.GetAxisRaw("LTrigger")  > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("RBumper"))
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("YButton"))
        {
            SwitchWeapon();
        }
        if(Input.GetMouseButton(1))
        {
            isAiming = true;
        } else 
        {
            isAiming = false;
        }

    }
}
