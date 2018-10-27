using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object respawns [respawnTimespan] seconds after being despawned

public class RespawnerScript : MonoBehaviour {
    [SerializeField] SpawnableBehaviour spawnable;
    [SerializeField]float respawnTimespan = 2f;

    float respawnTimestamp;

	void Awake () {
        if (spawnable != null)
        {
            Initialize();
            // Seperate RespawnerScript from Spawnable if both are on the same object
            if (spawnable.gameObject == this.gameObject) Seperate();
        }
    }

    void Update()
    {
        if (spawnable.IsActive == false && Time.time > respawnTimestamp) spawnable.Respawn();
    }

    void Initialize()
    {
        spawnable.OnDespawnEvent += OnDespawn;
        spawnable.isTracked = true;
    }

	void OnDespawn () {
        respawnTimestamp = Time.time + respawnTimespan;
    }

    void Seperate()
    {
        GameObject newGameObject = new GameObject();
        RespawnerScript newRespawnerScript = newGameObject.AddComponent<RespawnerScript>();
        newRespawnerScript.respawnTimespan = this.respawnTimespan;
        newRespawnerScript.spawnable = this.spawnable;
        newRespawnerScript.Initialize();
        Destroy(this);
    }
}
