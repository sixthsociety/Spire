using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour {

    public enum GunType { Semi, Burst, Auto};
    public GunType gunType;
    public float rpm;

    //Components
    public LineRenderer tracer;
    public Transform barrel;

    //System
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    private void Start()
    {
        //Heavy = GameObject.FindGameObjectWithTag("Heavy");
        secondsBetweenShots = 60 / rpm;
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(barrel.position, barrel.forward);
            RaycastHit hit;

            float shotDistance = 20;
            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                shotDistance = hit.distance;
                //distrance between origin and distance that it hit
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;
            Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

            if (tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }
        }
    }

    public void ShootContinuous()
    {
        if (gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        bool canShoot = true;
        if (Time.time < nextPossibleShootTime)
        {
            canShoot = false;
        }
        return canShoot;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, barrel.position);
        tracer.SetPosition(1, barrel.position + hitPoint);
        yield return null;
        tracer.enabled = false;
    }


}
