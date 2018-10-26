using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

    public const string playerSaveFile = "/player.sav";

    public static void SavePlayer (ObjectiveBehaviour player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + playerSaveFile, FileMode.Create);

        PlayerData data = new PlayerData(player);

        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static void LoadPlayer () 
    {
        if (File.Exists(Application.persistentDataPath + playerSaveFile))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + playerSaveFile, FileMode.Open);

            binaryFormatter.Deserialize(fileStream);
        }
    }
}

[Serializable]
public class PlayerData 
{
    public int[] level; // index 0 is level index 1 is exp 

    public PlayerData (ObjectiveBehaviour player) 
    {
        level = new int[2] { 0, 0};
    }
}