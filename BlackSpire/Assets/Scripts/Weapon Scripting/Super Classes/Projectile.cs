using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    [SerializeField] protected float speed; // how fast will the projectile travel (uses rb.AddForce)

    [SerializeField] protected Transform endPoint; // where any trails should start

    protected Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        OnShoot();
    }

    public virtual void OnShoot () 
    {
        rigidbody.AddForce(speed * transform.forward);
    }
}
