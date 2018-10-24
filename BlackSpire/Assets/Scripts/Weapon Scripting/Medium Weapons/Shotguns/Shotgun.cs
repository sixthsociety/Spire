using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MediumWeapon {

    [SerializeField] protected float maxSpread = 2f;
    [SerializeField] protected int shellAmount = 50;

    public override void DoShoot()
    {
        base.DoShoot();

        Vector3[] rays = new Vector3[shellAmount];

        for (int i = 0; i < shellAmount; i++)
        {
            RaycastHit hit;
            Vector3 direction = transform.forward;
            direction.x += Random.Range(-maxSpread, maxSpread);
            direction.y += Random.Range(-maxSpread, maxSpread);
            rays[i] = direction;

            Debug.Log("New Pellet");

            if (Physics.Raycast(firePoint.position, direction, out hit, range))
            {
                if (hit.collider.GetComponent<Enemy>())
                {
                    Debug.DrawRay(firePoint.position, direction);

                }
            }
        }

        for (int i = 0; i < shellAmount; i++)
        {
            Debug.DrawRay(firePoint.position, rays[i] * range);
        }
    }
}
