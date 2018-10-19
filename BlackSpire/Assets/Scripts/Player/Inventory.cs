using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 9;

    private List<InventoryItem> mItems = new List<InventoryItem>();

    public event EventHandler<InventoryItemsArgs> ItemAdded;

    public void AddItem(InventoryItem item)
    {
        if (mItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;

                mItems.Add(item);

                item.OnPickup();

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryItemsArgs(item));
                }

            }
        }
    }
}
