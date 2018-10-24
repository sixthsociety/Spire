using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class AmmoObject : MonoBehaviour {

    private enum AmmoType { LIGHT, MEDIUM, HEAVY };
    private AmmoType ammoType;

    [SerializeField] private float pickupRadius = 2;

    private void Start()
    {
        GetComponent<CapsuleCollider>().radius = pickupRadius;
    }

    public void SetAmmoType (int _ammoType) 
    {
        ammoType = (AmmoType) _ammoType;
    }

    private int ammoAmount = 1;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //PlayerWeapon playerWeapon = col.GetComponent<>().GetPlayerWeapon();

           //playerWeapon.PickUpAmmo(ammoAmount, (int) ammoType);
            Destroy(gameObject);
        }
    }
}
