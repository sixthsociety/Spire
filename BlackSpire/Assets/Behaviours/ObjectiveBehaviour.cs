using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// applied to all objectives
public class ObjectiveBehaviour : MonoBehaviour
{
    //[HideInInspector]
    public Objective m_ThisObjective; // assign this object and objective
    int kills;

    public bool m_IsCompleted { get; private set; }

    CombatBehaviour combat; // Change to private and set using GetCompInParent
    InventoryBehaviour inventory;

    private void Start()
    {
        combat = GetComponentInParent<CombatBehaviour>();
        combat.E_OnKill += UpdateQuests;
    }

    public delegate void CompletedEventHandler(object source, EventArgs e, Objective.ObjectiveType type);
    public event CompletedEventHandler CompletedEvent;

    // subbed to Combats OnKill method 
    public void UpdateQuests (object source, EventArgs e) 
    {
        for (int i = 0; i < m_ThisObjective.quests.Length; i++)
        {
            switch (m_ThisObjective.quests[i].questType)
            {
                case Quest.QuestType.Kill:
                    m_ThisObjective.quests[i].IncrementKills();
                    m_IsCompleted = false;
                    if (m_ThisObjective.quests[i].kills == m_ThisObjective.quests[i].totalKills)
                        m_ThisObjective.quests[i].Complete();
                        m_IsCompleted = true;
                    break;

                case Quest.QuestType.Collect :
                    m_IsCompleted = false;
                    break;
                case Quest.QuestType.Defeat :
                    m_IsCompleted = false;
                    break;
                case Quest.QuestType.Visit :
                    m_IsCompleted = false;
                    break;
            }
        }

        if (m_IsCompleted) 
        {
            OnCompleted();
        }
    }

    public void OnCompleted () 
    {
        if (CompletedEvent != null) 
        {
            CompletedEvent(this, EventArgs.Empty, m_ThisObjective.objectiveType);
        }
    }
}