using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableBehaviour : MonoBehaviour {
    public event System.Action OnRespawnEvent;
    public event System.Action OnDespawnEvent;

    public bool isTracked = false;

    public bool IsActive { get { return this.gameObject.activeInHierarchy; } }

    public SpawnableBehaviour Spawn(bool pooled)
    {
        this.isTracked = pooled;
        return Instantiate <SpawnableBehaviour>(this);
    }
    public void Respawn()
    {
        if (OnRespawnEvent != null) OnRespawnEvent();
        gameObject.SetActive(true);
    }
    public void Despawn () {
        if (OnDespawnEvent != null) OnDespawnEvent();
        if (isTracked) gameObject.SetActive(false);
        else Destroy(gameObject);
    }
}
