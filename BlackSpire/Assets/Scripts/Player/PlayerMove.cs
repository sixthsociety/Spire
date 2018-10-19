using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour {

    private float playerSpeed; // speed of player movement
    [SerializeField] float playerMoveSpeed = 0.08f;
    [SerializeField] float playerAimMoveSpeed = 0.04f;

    new Rigidbody rigidbody;
    [SerializeField] Camera mainCam;

    [HideInInspector] public bool canMove = true;

    private bool isAiming;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerSpeed = playerMoveSpeed;
    }

    void Move () 
    {
        float rot = Input.GetAxisRaw("Horizontal");
        float move = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(rot, 0f, move).normalized;

        transform.LookAt(transform.position + movement);
        rigidbody.MovePosition(rigidbody.position + movement * playerSpeed);
    }

    // responsible for calculating the way the player should be looking
    public void Aiming()
    {
        isAiming = true;
        // shoot a ray from cam through the mouse to the ground
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);

            // Look at point
            Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(heightCorrectedPoint);
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (Input.GetMouseButton(1))
        {
            Aiming();
        }
    }
}
