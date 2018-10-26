using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Objective", menuName ="Objective", order =1)]
public class Objective : ScriptableObject 
{
    public enum ObjectiveType { Level, OnGoing } // whether that objective is saved when the player leaves the level

    public string objectiveName = "def";
    public string description = "def";

    public ObjectiveType objectiveType;

    public Quest[] quests;

    #region Type Checks

    public bool hasCombat () 
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].questType == Quest.QuestType.Kill) 
            {
                return true;
            }
        }

        return false;
    }

    public bool hasCollect () 
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].questType == Quest.QuestType.Kill)
            {
                return true;
            }
        }

        return false;
    }

    public bool hasVisit()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].questType == Quest.QuestType.Kill)
            {
                return true;
            }
        }

        return false;
    }

    public bool hasDefeat()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].questType == Quest.QuestType.Kill)
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}

[CreateAssetMenu(fileName ="Quest", menuName ="Quest", order =2)]
public class Quest : ScriptableObject 
{
    public enum QuestType { Kill, Collect, Visit, Defeat }

    public string questDescription;

    public QuestType questType;
}
