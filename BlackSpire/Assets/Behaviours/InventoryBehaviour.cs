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

    public event System.Action onWeaponChange;

    // --- UNITY UPDATES ---

    void OnEnable () {
        SetWeapon(activeWeapon);
    }

    // --- PUBLIC ---

    // Note : Currently implemented for player only
    public void SetWeapon(WeaponType type)
    {
        if (combat == null) throw new System.Exception("Missing CombatBehaviour");

        activeWeapon = type;

        switch (type)
        {
            case WeaponType.LightWeapon:
                // Light weapons are when you run out of ammo or need to carry an object, and they provide no decrease to speed when aiming.
                combat.SetAttack(2, 0.05f);
                if (m_LogInEditor) Debug.Log("Weapon was set : LightWeapon");
                break;

            case WeaponType.MediumWeapon:
                // Medium weapons are mobile weapons you can move and fire(but movement speed when aiming is slowed)
                combat.SetAttack(20, 0.2f);
                if (m_LogInEditor) Debug.Log("Weapon was set : MediumWeapon");
                break;

            case WeaponType.HeavyWeapon:
                // Heavy weapons require you to stand in position and shoot(rocket launchers, mortars, miniguns etc)
                combat.SetAttack(50, 1f);
                if (m_LogInEditor) Debug.Log("Weapon was set : HeavyWeapon");
                break;

            case WeaponType.Grenade:
                // Grenades do not reduce speed but holding them past(as an example) 4 seconds causes them to detonate and damage the player.
                // (these also can be clocked back for throws without a hindrance to speed)
                combat.SetAttack(100,2f);
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


}
