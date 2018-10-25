using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pickups and switches

public class InteractableBehaviour : MonoBehaviour {
    [SerializeField] bool m_IsPickup = true;
    [SerializeField] bool m_CanRespawn = false;

    new public Collider collider;

    // --- UNITY UPDATES ---

	void Awake () {
        if (collider != null && m_IsPickup) collider.isTrigger = true;
    }

    // --- PUBLIC ---

    public bool CanBePickedUp()
    {
        return m_IsPickup;
    }

	public void PickUp () {
		
	}

    // --- PRIVATE ---

    void GetPickedUp()
    {
        if (m_CanRespawn)
        {
            // A game object can NOT turn itself back on once deactivated
            // (Unity updates are not called)
            // Requires SpawnBehaviour to re-enable
            this.gameObject.SetActive(false);
        }
        else
            Destroy(this.gameObject);
    }
}
