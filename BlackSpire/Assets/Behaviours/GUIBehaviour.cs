using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIBehaviour : MonoBehaviour {
    public InventoryBehaviour inventory;

    public Text text_DisplayAmmo;

    // --- Unity updates

    void OnEnable()
    {
        inventory.onWeaponChange += OnInventoryUpdate;
        inventory.onAmmoValueChange += OnInventoryUpdate;
    }

    // --- Public methods

    public void OnInventoryUpdate()
    {
        InventoryBehaviour.AmmoValue ammo = inventory.GetAmmoValue();
        text_DisplayAmmo.text = ammo.count + " / " + ammo.clipCount;
    }

    // --- Private methods

    void UnusedMethod () { }
}
