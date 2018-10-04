using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _equipped : MonoBehaviour {
    //Variables
    public bool equipped;
    public Transform inactive, light_inactive;
    public GameObject weapon_mesh, activeWep, passiveWep;
    private GameObject objectClass;
    public StateController sController;
    public Gun weaponScript;
    private WepController.WT weaponSwap;
    
    //Methods
    private void Start()
    {
        sController = GetComponentInParent<StateController>();
        weaponScript = GetComponent<Gun>();
        weaponSwap = GetComponentInParent<WepController.WT>();
    }


    void Update () {
        IfEquipped();
	}

    void IfEquipped()
    {
        if (equipped == true)
        {
            ActiveWeapon();
            weaponScript.enabled = true;
        }
        if (equipped == false)
        {
            InActiveWeapon();
            weaponScript.enabled = false;
        }
    }

    void ActiveWeapon()
    {
            if (sController.IsAiming == true)
            {
                weapon_mesh.transform.position = activeWep.transform.position;
                weapon_mesh.transform.rotation = activeWep.transform.rotation;
            }


            if (sController.IsAiming == false)
            {
                weapon_mesh.transform.position = passiveWep.transform.position;
                weapon_mesh.transform.rotation = passiveWep.transform.rotation;
            }
    }

    void InActiveWeapon()
    {
        weapon_mesh.transform.position = inactive.position;
        weapon_mesh.transform.rotation = inactive.rotation;
    }

}
