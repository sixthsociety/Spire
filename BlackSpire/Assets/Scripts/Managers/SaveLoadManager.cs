using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{
    public static void SaveObjectives(Objective[] objective)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/currentobjectives.sav", FileMode.Create);

        ObjectiveData objectiveData = new ObjectiveData(objective);

        binaryFormatter.Serialize(fileStream, objectiveData);
        fileStream.Close();
    }

    public static Objective[] LoadObjectives () 
    {
        if (File.Exists(Application.persistentDataPath + "/currentobjectives.sav"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/currentobjectives.sav", FileMode.Open);

            return (Objective[]) binaryFormatter.Deserialize(fileStream);
        }
        return null;
    }
}

[Serializable]
public class ObjectiveData
{
    public Objective[] objectives = new Objective[3];

    public ObjectiveData (Objective[] _objectives) 
    {
        objectives = _objectives;
    }
}