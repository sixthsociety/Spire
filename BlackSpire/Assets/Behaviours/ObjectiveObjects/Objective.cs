using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 

    The major objectives are assigned by the Level and do not save/aren't playable in other levels
    The minor objectives can be played in all level and are saved between games

*/

[CreateAssetMenu(fileName ="Objective", menuName ="Objective", order =1)]
public class Objective : ScriptableObject 
{
    public enum ObjectiveType { Major, Minor } // whether that objective is saved when the player leaves the level

    public string objectiveName = "def";
    public string description = "def";

    public ObjectiveType objectiveType;

    public Quest[] quests;

    // standard rewards
    public int exp;
    public int currency;
}

[CreateAssetMenu(fileName ="Quest", menuName ="Quest", order =2)]
public class Quest : ScriptableObject 
{
    public enum QuestType { Kill, Collect, Visit, Defeat }

    public string questDescription;

    public QuestType questType;

    public int totalKills;
    public int kills { get; private set; }

    public void IncrementKills() 
    {
        kills++;
    }

    public bool isCompleted { get; private set; }

    public void Complete () 
    {
        isCompleted = true;
    }
}
