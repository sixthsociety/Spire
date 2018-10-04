using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAnim : MonoBehaviour {

    //Variables
    public Transform ActiveWep, PassiveWep, CurrentWeapon, InactiveWep, lightHolder;
    public StateController sController;
    private WeaponManager weaponManager;
    public bool objectClass;
   

    //Methods
    void Start () {
        sController = GetComponentInParent<StateController>();
        weaponManager = GetComponentInParent<WeaponManager>();

        ActiveWep = GameObject.FindGameObjectWithTag("ActiveW").transform ;
        PassiveWep = GameObject.FindGameObjectWithTag("PassiveW").transform;
        InactiveWep = GameObject.FindGameObjectWithTag("Inactive Holder").transform;
        lightHolder = GameObject.FindGameObjectWithTag("LightHolder").transform;
        CurrentWeapon = gameObject.transform;
        
    }

    private void Update()
    {
        
    }

    void FixedUpdate () {
        WeaponSwap();
    }


    void WeaponSwap()
    {
        if (objectClass == true)
        {
            ActiveWeapon();
        }
        else if (objectClass == false)
        {
            InactiveWeapon();
        }
        return;
    }

    void ActiveWeapon()
    {
        if (sController.IsAiming == true)
        {
            CurrentWeapon.transform.position = ActiveWep.transform.position;
            CurrentWeapon.transform.rotation = ActiveWep.transform.rotation;
        }


        if (sController.IsAiming == false)
        {
            CurrentWeapon.transform.position = PassiveWep.transform.position;
            CurrentWeapon.transform.rotation = PassiveWep.transform.rotation;
        }
    }

    void InactiveWeapon()
    {
        if (objectClass = weaponManager.mediumWeapon)
        {
            CurrentWeapon.transform.position = InactiveWep.transform.position;
            CurrentWeapon.transform.rotation = InactiveWep.transform.rotation;
        }
        if (objectClass = weaponManager.heavyWeapon)
        {
            CurrentWeapon.transform.position = InactiveWep.transform.position;
            CurrentWeapon.transform.rotation = InactiveWep.transform.rotation;
        }
        if (objectClass = weaponManager.lightWeapon)
        {
            CurrentWeapon.transform.position = lightHolder.transform.position;
            CurrentWeapon.transform.rotation = lightHolder.transform.rotation;
        }
    }


}
