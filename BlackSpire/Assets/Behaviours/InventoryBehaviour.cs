using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pickups, purchasable, consumable, deployable, refill-station, interactable, reload

public class InventoryBehaviour : MonoBehaviour {

    [SerializeField] InventoryItemData[] inventorySlots;

    // List<InventoryItemData> resizableInventorySlots = new List<InventoryItemData>();

    // Note : inventory is the storage of items in data form (no GameObject)
    // When an item is put into the inventory, it becomes data
    // To place an item in toe level it gets Spawned
    //
    // When an item is picked up, it either is disabled(if pooled for example) or destroyed.
    // Inventory should therefor not keep a referance to the GameObject
    // Ask an item if it can be picked up, if it can, tell it to give you it's data, and tell it to handle it's pickup logic

    // Weapon's note (gameplay) :
    // Heavy weapons require you to stand in position and shoot(rocket launchers, mortars, miniguns etc)
    // Medium weapons are mobile weapons you can move and fire(but movement speed when aiming is slowed)
    // Light weapons are when you run out of ammo or need to carry an object, and they provide no decrease to speed when aiming.
    // Grenades do not reduce speed but holding them past(as an example) 4 seconds causes them to detonate and damage the player.
    // (these also can be clocked back for throws without a hindrance to speed)

    // --- UNITY UPDATES ---

    void OnEnable () {
        
    }

    // --- PUBLIC ---

    public void Update () {
		
	}

    // --- PRIVATE ---

}

public class InventoryItemData
{
    public string editorName;
    public int indexInInventory;
    public GameObject prefab;
    public int count = 1;
    public int maxCount = 1;
    // There needs to be enough data in here to spawn it
}
