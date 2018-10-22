using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class stores values for the spawn pos and rarites for each location
public class AmmoSpawn : MonoBehaviour {

    [SerializeField] private GameObject ammo;

    [SerializeField] float lightSpawn = 0.5f;
    [SerializeField] float mediumSpawn = 0.6f;
    [SerializeField] float heavySpawn = 0.2f;

    private float[] weights;

    private void Start()
    {
        weights = new float[] { lightSpawn, mediumSpawn, heavySpawn };

        SpawnAmmo();
    }

    void SpawnAmmo () 
    {
        GameObject newAmmo = Instantiate(ammo, transform.position, transform.rotation, transform);
        newAmmo.GetComponent<AmmoObject>().SetAmmoType(GetAmmoType());
    }

    int GetAmmoType()
    {
        float choice = Random.value * (lightSpawn + mediumSpawn + heavySpawn);
        for (int i = 0; i < weights.Length; i++)
        {
            choice -= weights[i];
            if (choice < 0)
                return i;
        }

        Debug.LogError("Incorrect weights at AmmoSpawn");
        return 0;
    }
}
