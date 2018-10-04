using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour {

    public StateController state;
    private float rayDistance;
    public Transform activeWeapon;

    private LineRenderer lr;
    // Use this for initialization

    void Start () {
        lr = GetComponent<LineRenderer>();
        state = GetComponentInParent<StateController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = activeWeapon.position;
        gameObject.transform.rotation = activeWeapon.rotation;

        if (state.IsAiming == true)
        {
            lr.enabled = true;
            rayDistance = 20f;
            lr.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                }
            }
            else lr.SetPosition(1, transform.forward * 500);
        }



        if (state.IsAiming == false)
        {
            lr.enabled = false;
        }



    }

}
