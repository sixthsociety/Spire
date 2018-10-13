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
        currentWeapon.ShootTrigger();
    }

    void Reload () 
    {
        currentWeapon.ReloadTrigger();
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
    }
}
