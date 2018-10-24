using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// rigged-character-animations, non-humanoid-animations, material-color-change, partacles

public class AnimationBehaviour : MonoBehaviour {
    public InventoryBehaviour inventory;

    [SerializeField] GameObject m_Model_Light, m_Model_Medium, m_Model_Heavy, m_Model_Grenade;


    void OnEnable () {
        if (inventory != null) inventory.onWeaponChange += OnWeaponChange;

    }
	

	public void OnWeaponChange () {
        m_Model_Light.SetActive(inventory.ActiveWeapon == InventoryBehaviour.WeaponType.LightWeapon);
        m_Model_Medium.SetActive(inventory.ActiveWeapon == InventoryBehaviour.WeaponType.MediumWeapon);
        m_Model_Heavy.SetActive(inventory.ActiveWeapon == InventoryBehaviour.WeaponType.HeavyWeapon);
        m_Model_Grenade.SetActive(inventory.ActiveWeapon == InventoryBehaviour.WeaponType.Grenade);
	}
}
