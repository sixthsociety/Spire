using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAnim : MonoBehaviour {

    //Variables
    public Transform ActiveWep, PassiveWep, CurrentWeapon, InactiveWep, lightHolder;
    public StateController sController;
    public bool objectClass;
    public PlayerWeapon equippedWeapon;

    //Methods
    void Start () {
        sController = GetComponentInParent<StateController>();
        equippedWeapon = GetComponent<PlayerWeapon>();

        ActiveWep = GameObject.Find("ActiveWeapon").transform ;
        PassiveWep = GameObject.Find("PassiveWeapon").transform;
        InactiveWep = GameObject.Find("InActive Holder").transform;
        lightHolder = GameObject.Find("InActive Holder 2").transform;
        CurrentWeapon = gameObject.transform;
        
    }

    void weaponPlacement()
    {
        //if (equippedWeapon.Equip)
    }
}
