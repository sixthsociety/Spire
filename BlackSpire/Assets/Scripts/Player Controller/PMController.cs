using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMController : MonoBehaviour {
    //Variables
    InputManager inputManager;

    public Rigidbody rigidBody;

    public float rotateSpeed = 6f;

    private Vector3 moveDirection;

    public float moveSpeed;

    public bool IsAiming;

    public Transform cam;

    public GameObject playerModel, pivot;

    public Animator anim;

    public Component AimController;

    //Methods
    private void Start()
    {
        pivot = GameObject.Find("Pivot");
        inputManager = GetComponent<InputManager>();
        rigidBody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        AimController = GetComponent<AimController>();
    }


    private void FixedUpdate()
    {
        Aiming();
        PassiveMovement();
        SetMoveSpeed();
        SetSpeed();
        UpdateAnimator();
    }


    void Aiming()
    {
        //See If RightStick is Active. To be improved
        if (Input.GetAxisRaw("RHorizontal") != 0 | Input.GetAxisRaw("RVertical") != 0)
        {
            IsAiming = true;
        }
        else
        {
            IsAiming = false;
        }
    }



    void PassiveMovement()
    {
        moveDirection = (transform.forward * inputManager.Vertical) + (transform.right * inputManager.Horizontal);
        moveDirection = moveDirection.normalized * moveSpeed;

        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime);

        rigidBody.velocity = (moveDirection * moveSpeed * Time.deltaTime);
        //Move the player in different directions based on camera look direction
        if (inputManager.Horizontal != 0 || inputManager.Vertical != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

        }
    }

    void SetMoveSpeed()
    {
        moveSpeed = (Mathf.Abs(inputManager.Vertical) + Mathf.Abs(inputManager.Horizontal)) * 18;
        if (moveSpeed >= 18)
            moveSpeed = 18;
    }

    void SetSpeed()
    {
        anim.SetFloat("Speed", (Mathf.Abs(inputManager.Vertical)) + (Mathf.Abs(inputManager.Horizontal)));
    }

    void UpdateAnimator()
    {
        anim.SetBool("IsAiming", false);
    }

}
