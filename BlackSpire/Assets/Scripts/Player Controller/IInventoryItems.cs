using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get;  }
    
    Sprite Image { get; }

    void OnPickup();
}

public class InventoryItemsArgs : EventArgs {

    public InventoryItemsArgs(IInventoryItem item)
    {
        Item = item;
    }

    public IInventoryItem Item;
}
