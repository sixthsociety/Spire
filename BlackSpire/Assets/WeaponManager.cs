using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    //Variables
    public bool heavyWeapon;
    public bool mediumWeapon;
    public bool lightWeapon;
    private InputManager inputManager;
    private Animator anim;
    public float weaponSwapTimer = 0;

    //Methods
    void Start () {

        heavyWeapon = false;
        mediumWeapon = true;
        lightWeapon = false;
        inputManager = GetComponentInParent<InputManager>();
        anim = GetComponentInParent<Animator>();
    }
	
	
	void FixedUpdate () {
        SelectWeapon();
        Animate();
    }

    void SelectWeapon()
    {
        //Cycling Medium and Heavy
        if (Input.GetButtonUp("YButton") && mediumWeapon == true && heavyWeapon == false)
        {
            mediumWeapon = false;
            heavyWeapon = true;
            lightWeapon = false;
        }
        else if (Input.GetButtonUp("YButton") && heavyWeapon == true && mediumWeapon == false) 
        {
            mediumWeapon = true;
            heavyWeapon = false;
            lightWeapon = false;
        }
        if (Input.GetButton("YButton") && lightWeapon == false)
        {
            weaponSwapTimer += Time.deltaTime;
        }
        if (Input.GetButtonUp("YButton") && weaponSwapTimer > 2)
        {
            lightWeapon = true;
            mediumWeapon = false;
            heavyWeapon = false;
            weaponSwapTimer = 0;
        }
        if(Input.GetButtonUp("YButton") && weaponSwapTimer < 2)
        {
            weaponSwapTimer = 0;
        }
    }


    void Animate()
    {
        if (Input.GetButtonUp("YButton"))
        {
            anim.SetBool("Swep", true);
        }
    }
}
