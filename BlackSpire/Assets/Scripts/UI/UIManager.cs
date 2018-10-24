using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour {

    [SerializeField] private HealthBehaviour hB;

    private void Start()
    {
        hB.Died += OnDie;
    }

    public virtual void OnDie (object health, EventArgs args) 
    {
        PlayerDied();
    }

    void PlayerDied() 
    {
        Debug.Log("UIManager: Player Died"); 
    }
}
