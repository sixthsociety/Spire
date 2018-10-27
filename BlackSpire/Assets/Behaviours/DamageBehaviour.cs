using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : MonoBehaviour {
    public enum DamageBehaviourType {none, raycast, sphere, collision, detonated}
    public DamageBehaviourType damageBehaviourType = DamageBehaviourType.none;

    public Transform blastZoneIndicator;

    public int baseDamage = 0;

    float blastTime = 0;
    //public

	// Use this for initialization
	void OnEnable () {
        blastTime = Time.time + 2f;

    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > blastTime)
        {
            this.enabled = false;
            Rigidbody[] rigidbodies = Object.FindObjectsOfType<Rigidbody>();
            foreach (Rigidbody r in rigidbodies)
                r.AddExplosionForce(10f, this.transform.position, 5f, 0.1f, ForceMode.Impulse);
        }
	}

    void FixedUpdate()
    {

    }

    void OnCollision(Collision collision)
    {
        if (damageBehaviourType != DamageBehaviourType.collision) return;
    }
}
