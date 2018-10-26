using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// applied to all objectives
public class ObjectiveBehaviour : MonoBehaviour
{
    public Objective m_ThisObjective; // assign this object and objective
    int kills;

    public bool m_IsCompleted { get; private set; }

    public CombatBehaviour combat; // Change to private and set using GetCompInParent
    InventoryBehaviour inventory;

    private void Start()
    {
        combat.E_OnKill += UpdateQuests;
    }

    public void UpdateQuests (object source, EventArgs e) 
    {
        if (m_ThisObjective.hasCombat())
        {
            kills++;
            Debug.Log(kills);
        }
    }
}