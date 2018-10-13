using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {

    //Variables
    public Animator anim;
    public GameObject playerModel;

    public Rigidbody rigidBody;
    public Transform cam;
    public float moveSpeed;
    public bool IsAiming;

    //public WepController weapon;
    //public Gun gun;

    private Vector3 moveDirection;
    private Vector3 camForward;
    private Vector3 movement;
    private Vector3 moveInput;

    private float forwardAmount;
    private float turnAmount;

    InputManager inputManager;

    //Methods
    void Start () {
        inputManager = GetComponent<InputManager>();
        rigidBody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();
    }
	
	void FixedUpdate () {
        Aiming();
        ADSmovement();
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



    void ADSmovement()
    {
        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            movement = inputManager.Vertical * camForward + inputManager.Horizontal * cam.right;
        }
        else
        {
            movement = inputManager.Vertical * Vector3.forward + inputManager.Horizontal * Vector3.right;
        }
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        movement = movement * moveSpeed;

        //Movement Via Character Controller
        rigidBody.velocity = (movement * moveSpeed * Time.deltaTime);

        //RightStick Movement
        Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            playerModel.transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);

        }
        Move(movement);
        SetSpeed();
    }

    void Move(Vector3 movement)
    {
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        //to be used in conversion for Animation
        this.moveInput = movement;
        ConvertMoveInput();
        UpdateAnimator();

    }
    void ConvertMoveInput()
    {
        Vector3 localMove = playerModel.transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwardAmount = localMove.z;
    }

    void UpdateAnimator()
    {
        anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        anim.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        anim.SetBool("IsAiming", true);
    }

    void SetSpeed()
    {
        anim.SetFloat("Speed", (Mathf.Abs(inputManager.Vertical)) + (Mathf.Abs(inputManager.Horizontal)));
    }
}
