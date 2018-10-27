using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    // Variables
    //public Rigidbody targetRigidbody;
    public MovementBehaviour movement;
    public GameObject target;
    public GameObject pivot;

    public Vector3 cameraOffset = new Vector3(0f, 9f, -7);

    public float smooth = 0.3f;
    private float desiredYAngle;
    private float desiredXAngle;

    private Vector3 velocity = Vector3.zero;

    public Vector3 offset;



    //Methods

    private void Start()
    {
        if (pivot == null) pivot = GameObject.Find("Pivot");
        if (pivot == null) pivot = new GameObject();
        if(target == null) target = GameObject.Find("Player");

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

        //target.transform.forward * 
        Vector3 pos = target.transform.position + movement.moveDirection * 2f;//new Vector3();
        //pos.x = target.transform.position.x;
        //pos.z = target.transform.position.z - 7f;
        //pos.y = target.transform.position.y + 9f;
        pos += cameraOffset;
        //velocity = targetRigidbody.velocity;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);

    }
}
