using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissileScript : MonoBehaviour {
    public SpawnableBehaviour spawnable;
    public Transform homingTarget;
    public Vector3 homingPosition;
    public float homongRotationSpeed = 90f;
    public bool isHoming = false;
    public float speed = 1f;

	// Use this for initialization
	void Start () {
        if (spawnable == null) spawnable = GetComponent<SpawnableBehaviour>();

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {

        if (isHoming)
        {
            if (homingTarget != null) homingPosition = homingTarget.position + Vector3.up;
            Quaternion r = transform.rotation;
            Quaternion lookRotation = Quaternion.LookRotation(homingPosition - transform.position);
            r = Quaternion.RotateTowards(r, lookRotation, homongRotationSpeed * Time.fixedDeltaTime);
            transform.rotation = r;
        }

        Vector3 p = this.transform.position;
        float dist = Time.fixedDeltaTime * speed;
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(p, transform.forward, out hitInfo, dist, -1, QueryTriggerInteraction.Ignore);
        if (isHit)
        {
            p = hitInfo.point;
            spawnable.Despawn();
        }
        else
        {
            p += transform.forward * dist;
        }
        transform.position = p;
    }
}
