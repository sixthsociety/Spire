using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Objectives, Timers, quest conditions
public class MissionBehaviour : MonoBehaviour
{
    public enum QuestType {Kills, Visit, Collect}

    private Quest m_DefaultQuest;
    private bool m_HasQuests;

    // the quests are side missions that are randomly selected for the player to gain exp and currency they dont progress the player in campaign
    // offer a side mission feel
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 1)]
    public class Quest : ScriptableObject
    {
        public string objectiveName;
        public string description;

        public QuestType questType;
    }

    // Current Quests on the Player
    public List<Quest> allObjectives = new List<Quest>();
    public Quest[] currentObjectives = new Quest[3];

    private void Start()
    {
        currentObjectives[0] = m_DefaultQuest;
        currentObjectives[1] = m_DefaultQuest;
        currentObjectives[2] = m_DefaultQuest;

        SetNewObjective(0);
    }

    void LoadQuests () 
    {
        // Load the players saved quests
    }

    void SetNewObjective (int toReplace) 
    {
        if (allObjectives.Count != 0 && toReplace <= currentObjectives.Length)
        {
            currentObjectives[toReplace] = allObjectives[Random.Range(0, allObjectives.Count)];
        } else if (allObjectives.Count <=0)
        {
            throw new System.Exception("MissionBehaviour: All Objectives is null! You must add objectives!");
        } else if(toReplace > currentObjectives.Length)
        {
            throw new System.Exception("MissionBehaviour: Cannot replace a non-existent quest! Fix the quest index.");
        }
    }
}