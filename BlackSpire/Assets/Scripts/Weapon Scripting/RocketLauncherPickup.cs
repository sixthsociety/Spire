using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherPickup : MonoBehaviour, IInventoryItem
{

    public string Name
    {
        //gets name
        get
        {
            ///enter weapon name here
            return "RocketLauncher";
        }
    }

    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }
}
