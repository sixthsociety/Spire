using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    private WeaponSpawn[] spawns;

    void SpawnItems () 
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            WeaponSpawn thisSpawn = spawns[i];
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        spawns = FindObjectsOfType<WeaponSpawn>();
        
    }
}
