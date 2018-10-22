using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{

    protected enum AmmoType { LIGHT = 0, MEDIUM = 1, HEAVY = 3 };
    protected AmmoType ammoType;

    protected enum Rarity { COMMON = 0, UNCOMMON = 1, RARE = 2, ULTRA_RARE = 3, LEGENDARY = 4 };

    [SerializeField] protected string gunName; //name to be displayed in UI

    [SerializeField] protected int clipSize = 100; //total ammo gun can hold
    protected int totalAmmo;
    protected int loadedAmmo; //current ammo left in clip

    [SerializeField] protected float fireRate = 0.1f; //time between shots (seconds)
    [SerializeField] protected float reloadDelay = 1f; // time it takes to reload (seconds)

    [SerializeField] protected int damage = 10;

    [SerializeField] protected float range = 20f; // the max dist of bullet

    protected int level = 1; // the level of the gun (1 - 3)
    protected int kills = 0; // amount of kills with the gun

    protected bool canShoot = true; // true if the player is allowed to shoot
    protected bool isReloading = false; // true is the player is currently reloading

    [SerializeField] protected Transform firePoint; // where the bullet is fired from

    private void Start()
    {
        loadedAmmo = clipSize;
    }

    public string GetName () 
    {
        return gunName;
    }

    public int GetCurrentAmmo () 
    {
        return loadedAmmo;
    }

    public int GetClipSize () 
    {
        return clipSize;
    }

    public int GetAmmoType () 
    {
        return (int) ammoType;
    }

    //Set the ammo type, used by child classes
    protected void SetAmmoType (int _ammoType)
    {
        ammoType = (AmmoType) _ammoType;
    }

    //Called by playerWeapon method in order to shoot gun
    public void ShootTrigger () 
    {
        if (canShoot && loadedAmmo > 0)
        {
            StartCoroutine(ShootTime());
        }
        else if (loadedAmmo <= 0 && isReloading == false)
        {
            Debug.Log("You need to RELOAD!");
        }
    }

    // use ienumerator to time the shots
    protected IEnumerator ShootTime () 
    {
        canShoot = false;
        DoShoot();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public virtual void DoShoot () 
    {
        loadedAmmo--;
    }

    public void ReloadTrigger()
    {
        StartCoroutine(ReloadTime());
    }

    protected IEnumerator ReloadTime ()
    {
        isReloading = true;
        canShoot = false;
        yield return new WaitForSeconds(reloadDelay / 2);
        DoReload();
        yield return new WaitForSeconds(reloadDelay / 2);
        canShoot = true;
        isReloading = false;
    }

    public virtual void DoReload () 
    {
        //TODO : add an if check to see if the player has enough ammo to reload
        if (totalAmmo > clipSize)
        {
            loadedAmmo = clipSize;
            totalAmmo -= clipSize;
        } else 
        {
            loadedAmmo = totalAmmo;
            totalAmmo = 0;
        }
    }

    public void AddAmmo (int _amount)
    {
        totalAmmo += _amount;
        Debug.Log("Added ammo to " + gameObject.name + " weapon");
    }
}
