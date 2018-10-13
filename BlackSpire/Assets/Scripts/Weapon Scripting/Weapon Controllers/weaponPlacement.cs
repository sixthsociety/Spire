using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPlacement : MonoBehaviour {
    [SerializeField]
    private Transform thisItem;
    public Transform passiveHolder, activeHolder;
    [SerializeField]
    private StateController state;

	
	void Start () {
        thisItem = GetComponent<Transform>();
        passiveHolder = GameObject.Find("PassiveWeapon").transform;
        activeHolder = GameObject.Find("ActiveWeapon").transform;
    }
	

	void Update () {
        PositionWeapon();
	}

    void PositionWeapon()
    {
        if (state.IsAiming == true)
        {
            thisItem.position = activeHolder.position;
            thisItem.rotation = activeHolder.rotation;
        }
        else
        {
            thisItem.position = passiveHolder.position;
            thisItem.rotation = passiveHolder.rotation;
        }
    }
}
