using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

    // Variables
    public GameObject target;
    public GameObject pivot;
    public float smooth = 0.3f;
    private float desiredYAngle;
    private float desiredXAngle;

    private Vector3 velocity = Vector3.zero;

    public Vector3 offset;



    //Methods

    private void Start()
    {
        target = GameObject.Find("Player");
        pivot = GameObject.Find("Pivot");
        offset = target.transform.position - transform.position;

        pivot.transform.position = target.transform.position;
        //pivot.parent = target;
        pivot.transform.parent = null;
    }


    private void LateUpdate()
    {
        pivot.transform.position = target.transform.position;
        //Pivot Rotation
        desiredYAngle = pivot.transform.eulerAngles.y;
        desiredXAngle = pivot.transform.eulerAngles.x;

        Vector3 pos = new Vector3();
        pos.x = target.transform.position.x;
        pos.z = target.transform.position.z - 7f;
        pos.y = target.transform.position.y + 9f;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);

    }

}
