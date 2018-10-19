using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventoryController : MonoBehaviour , InventoryItem {

    public string Name
    {
        get
        {
            return "RocketLauncher";
        }
    }

    public Sprite _Image;

    public Sprite Image
    {
        get { return _Image; }
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }


}
