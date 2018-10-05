using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile {

    [SerializeField] protected ParticleSystem rocketTrail; // the trail of fire and smoke the rocket leaves behind

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        OnShoot();
    }

    public override void OnShoot()
    {
        base.OnShoot();

        Instantiate(rocketTrail, endPoint.position, endPoint.rotation);
    }
}
