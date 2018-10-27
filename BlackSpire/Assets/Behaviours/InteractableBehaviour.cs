using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pickups and switches

public class InteractableBehaviour : MonoBehaviour {
    [SerializeField] bool m_IsPickup = true;
    [SerializeField] bool m_CanRespawn = false;
    [SerializeField] Renderer m_EditorMaterial;
    [SerializeField] SpawnableBehaviour m_Spawnable;

    public enum InteractionRewardType { None, Light, Medium, Heavy, Grenade};
    public InteractionRewardType rewardType = InteractionRewardType.None;
    public int rewardQuantity = 0;

    new public Collider collider;

    // --- UNITY UPDATES ---

	void Awake () {
        if (collider != null && m_IsPickup) collider.isTrigger = true;
        if (m_EditorMaterial != null) m_EditorMaterial.enabled = false;
    }

    // --- PUBLIC ---

    public bool CanBePickedUp(InventoryBehaviour inventory)
    {
        // To do : check if ammo is not already full
        return m_IsPickup;
    }

	public void PickUp (InventoryBehaviour inventory) {
        GetPickedUp(inventory);
    }

    // --- PRIVATE ---

    void GetPickedUp(InventoryBehaviour inventory)
    {
        if (rewardType != InteractionRewardType.None && rewardQuantity > 0)
        {
            // Add ammo of type to inventory
            // inventory
            Debug.Log(inventory.name + " <---");
        }

        if(m_Spawnable!=null) m_Spawnable.Despawn();
    }
}
