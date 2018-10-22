using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour {

    Animator animator;

    private float playerSpeed; // speed of player movement
    [SerializeField] float playerMoveSpeed = 0.08f;
    [SerializeField] float playerAimMoveSpeed = 0.04f;

    new Rigidbody rigidbody;
    [SerializeField] Camera mainCam;

    [HideInInspector] public bool canMove = true;

    private PlayerWeapon playerWeapon;

    private Vector3 inputDirection;
    private Vector3 inputWorldPosition;
    private bool isAiming;
    private Quaternion lookRotation = Quaternion.identity;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerSpeed = playerMoveSpeed;

        animator = this.GetComponentInChildren<Animator>();

        playerWeapon = GetComponentInChildren<PlayerWeapon>();
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector3(inputX, 0f, inputY).normalized;

        isAiming = playerWeapon.GetAim();

        // responsible for calculating the way the player should be looking
        if (isAiming)
        {
            // shoot a ray from cam through the mouse to the ground
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                inputWorldPosition = ray.GetPoint(rayDistance);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAiming)
        {
            Look(inputWorldPosition - transform.position);
            Move(inputDirection);
        }
        else
        {
            Look(inputDirection);
            Move(inputDirection);
        }
    }

    void Move(Vector3 direction)
    {
        //Animation
        Vector3 localDirection = Quaternion.Inverse(lookRotation) * direction;
        animator.SetFloat("MoveX", localDirection.x);
        animator.SetFloat("MoveY", localDirection.z);

        rigidbody.MovePosition(rigidbody.position + direction * playerSpeed);
    }

    void Look(Vector3 direction)
    {
        if (direction.sqrMagnitude < Mathf.Epsilon) return;
        direction.y = 0f;
        lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = lookRotation;
    }
}
