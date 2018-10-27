using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour {

    public GameObject objectivePrefab;
    public Objective tmpObjective;

    ObjectiveBehaviour majorObjective;

    ObjectiveBehaviour[] minorObjectives = new ObjectiveBehaviour[3];

    private void Start()
    {
        Objective[] loadedObjectives = SaveLoadManager.LoadObjectives();
        for (int i = 0; i < minorObjectives.Length; i++)
        {
            SetObjective(loadedObjectives[i]);
        }
    }

    //set objective is called when player faces new major objective.  This can be called by a level that has a set objective.
    public void SetObjective(Objective newObjective)
    {
        GameObject newObjectiveObject = Instantiate(objectivePrefab, transform.position, transform.rotation, transform);

        ObjectiveBehaviour objectiveBehaviour = newObjectiveObject.GetComponent<ObjectiveBehaviour>();
        objectiveBehaviour.m_ThisObjective = newObjective;

        switch (newObjective.objectiveType)
        {
            case Objective.ObjectiveType.Major :
                if (majorObjective != null)
                {
                    Debug.Log("Trying to override the current objective... all progress will be lost!");
                }

                majorObjective = objectiveBehaviour;
                majorObjective.CompletedEvent += ObjectiveCompleted;
                break;

            case Objective.ObjectiveType.Minor :
                for (int i = 0; i < minorObjectives.Length; i++)
                {
                    if (minorObjectives[i] == null)
                    {
                        minorObjectives[i] = objectiveBehaviour;
                    }
                }

                Debug.LogWarning("ObjectiveManager: You are trying to add a new objective when you have no available slots! Abandon a minor objective before trying again.");
                break;
        }   

    }

    //TODO apply rewards to the player
    public virtual void ObjectiveCompleted (object source, EventArgs e, Objective.ObjectiveType type)
    {
        Debug.Log("You Completed the Objective!");

        switch (type) 
        {
            case Objective.ObjectiveType.Major :
                if (majorObjective != null)Destroy(majorObjective.gameObject);
                break;
            case Objective.ObjectiveType.Minor :
                for (int i = 0; i < minorObjectives.Length; i++)
                {
                    if(minorObjectives[i] == (ObjectiveBehaviour) source)
                    {
                        Destroy(minorObjectives[i].gameObject);
                    }
                }
                break;
        }
    }

    private void OnApplicationQuit()
    {
        Objective[] toSave = new Objective[3];
        for (int i = 0; i < minorObjectives.Length; i++)
        {
            toSave[i] = minorObjectives[i].m_ThisObjective;
        }
        SaveLoadManager.SaveObjectives(toSave);
    }
}
