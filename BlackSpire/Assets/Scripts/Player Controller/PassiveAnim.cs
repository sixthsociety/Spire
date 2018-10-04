using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAnim : MonoBehaviour {

    //Variables
    public Transform PassiveWeapon, CurrentWeapon;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CurrentWeapon.transform.position = PassiveWeapon.transform.position;
        CurrentWeapon.transform.rotation = PassiveWeapon.transform.rotation;
    }
}
