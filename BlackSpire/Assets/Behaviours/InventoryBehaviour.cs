using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pickups, purchasable, consumable, deployable, refill-station, interactable, reload

public class InventoryBehaviour : MonoBehaviour {


    public enum WeaponType {Debug, LightWeapon, MediumWeapon, HeavyWeapon, Grenade }

    [SerializeField] WeaponType activeWeapon = WeaponType.LightWeapon;
    public WeaponType ActiveWeapon { get { return activeWeapon; } }

    [SerializeField] bool m_LogInEditor = false; 
    public CombatBehaviour combat;

    // Sugestion : store in array with an accessor that uses WeaponType as index
    [System.Serializable] public struct AmmoValue{ public int count, clipCount, clipSize, maxClips; public int total {get{ return count + (clipCount * clipSize); } } }
    public AmmoValue lightAmmo, mediumAmmo, heavyAmmo, grenadeAmmo;

    // sugestion : call all events EventEventName instead of onEventName
    // To do : make weapon switching non-instant (EventWeaponStartChange, EventWeaponChanged)
    public event System.Action onWeaponChange;
    public event System.Action onAmmoValueChange;

    // --- UNITY UPDATES ---

    void OnEnable () {
        SetWeapon(activeWeapon);
    }

    void OnTriggerEnter(Collider collider)
    {
        InteractableBehaviour interactable = collider.GetComponent<InteractableBehaviour>();
        if (interactable != null) HandleInteractable(interactable);
    }

    // --- PUBLIC ---

    public CombatBehaviour GetActiveCombat()
    {
        // should return CombatBehaviour based on the active combat type
        return combat;
    }

    public void AddAmmoClips(int clipCount)
    {
        switch (activeWeapon)
        {
            case WeaponType.LightWeapon: lightAmmo.clipCount += clipCount; break;
            case WeaponType.MediumWeapon: mediumAmmo.clipCount += clipCount; break;
            case WeaponType.HeavyWeapon: heavyAmmo.clipCount += clipCount; break;
            case WeaponType.Grenade: grenadeAmmo.clipCount += clipCount; break;
            default: throw new System.Exception("Invalid ammo type");
        }
    }

    public AmmoValue GetAmmoValue()
    {
        switch (activeWeapon)
        {
            case WeaponType.LightWeapon: return lightAmmo;
            case WeaponType.MediumWeapon: return mediumAmmo;
            case WeaponType.HeavyWeapon: return heavyAmmo;
            case WeaponType.Grenade: return grenadeAmmo;
            default:throw new System.Exception("Invalid ammo type");
        }
    }

    // Note : Currently implemented for player only
    // To change : combat behaviour per weapon instead of one for all types
    public void SetWeapon(WeaponType type)
    {
        if (combat == null) throw new System.Exception("Missing CombatBehaviour");

        activeWeapon = type;

        switch (type)
        {
            case WeaponType.LightWeapon:
                // Light weapons are when you run out of ammo or need to carry an object, and they provide no decrease to speed when aiming.
                combat.SetAttack(2, 0.05f, CombatBehaviour.AttackType.Bullets);
                if (m_LogInEditor) Debug.Log("Weapon was set : LightWeapon");
                break;

            case WeaponType.MediumWeapon:
                // Medium weapons are mobile weapons you can move and fire(but movement speed when aiming is slowed)
                combat.SetAttack(20, 0.2f, CombatBehaviour.AttackType.Bullets);
                if (m_LogInEditor) Debug.Log("Weapon was set : MediumWeapon");
                break;

            case WeaponType.HeavyWeapon:
                // Heavy weapons require you to stand in position and shoot(rocket launchers, mortars, miniguns etc)
                combat.SetAttack(50, 1f, CombatBehaviour.AttackType.Missile);
                if (m_LogInEditor) Debug.Log("Weapon was set : HeavyWeapon");
                break;

            case WeaponType.Grenade:
                // Grenades do not reduce speed but holding them past(as an example) 4 seconds causes them to detonate and damage the player.
                // (these also can be clocked back for throws without a hindrance to speed)
                combat.SetAttack(100,2f, CombatBehaviour.AttackType.Grenade);
                if (m_LogInEditor) Debug.Log("Weapon was set : Grenade");
                break;

            case WeaponType.Debug: throw new System.Exception("Bad weapon, don't do this");
        }

        if (onWeaponChange != null) onWeaponChange();
    }

    public void Attack()
    {
        // Check ammo
    }

    // --- PRIVATE ---

    void HandleInteractable(InteractableBehaviour interactable)
    {
        if (interactable.CanBePickedUp(this) == false) { Debug.Log("Action pick-up failed"); return; }

        // Handle pickup
        interactable.PickUp(this);

        //Debug.Log(interactable.name);
    }
}
