using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MediumWeapon {

    [SerializeField] protected float maxSpread = 2f;
    [SerializeField] protected int shellAmount = 50;

    public override void DoShoot()
    {
        base.DoShoot();

        for (int i = 0; i < shellAmount; i++)
        {
            RaycastHit hit;
            Vector3 direction = transform.forward;
            direction.x = Random.Range(-maxSpread, maxSpread);
            direction.y = Random.Range(-maxSpread, maxSpread);
            direction.z = Random.Range(-maxSpread, maxSpread);

            if (Physics.Raycast(firePoint.position, direction, out hit, range))
            {
                //if (hit.collider.GetComponent<Enemy>())
                {
                    //hit.collider.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }
    }
}
