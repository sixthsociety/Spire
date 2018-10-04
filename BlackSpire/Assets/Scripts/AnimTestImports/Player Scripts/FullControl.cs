using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullControl : MonoBehaviour {
    //Variables
    public GameObject playerModel;
    public CharacterController controller;
    public Transform pivot, cam;
    public Animator anim;


    //public floats
    public float rotateSpeed;
    public float moveSpeed;
    //public bools
    public bool IsAiming;
    public bool useController;
    public bool isSprinting;
    //private vectors
    private Vector3 moveDirection;
    private Vector3 camForward;
    private Vector3 movement;
    private Vector3 moveInput;
    //Private Floats
    private float forwardAmount;
    private float turnAmount;
    private float h;
    private float v;
    //Debug Console





    //Methods
    void Start () {
        controller = GetComponent<CharacterController>();
        IsAiming = false;
        isSprinting = false;
        cam = Camera.main.transform;
    }
	



	// Update
	void Update () {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //Check For Aiming
        Aiming();
        IsSprinting();

        if (IsAiming)
        {
            
            isSprinting = false;
            ADSmovement();
            moveSpeed = 2;
            anim.SetBool("IsAiming", true);
        }
        if (!IsAiming && !isSprinting)
        {
         
            PassiveMovement();
            anim.SetBool("IsAiming", false);
            SetMoveSpeed();
        }
        if (!IsAiming && isSprinting)
        {
           
            PassiveMovement();
            moveSpeed = 10;
            anim.SetBool("IsAiming", false);
        }
        
    }






    //Check for Aiming
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


    //Aiming Down Sights Movement Build
    void ADSmovement()
    {
        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            movement = v * camForward + h * cam.right;
        }
        else
        {
            movement = v * Vector3.forward + h * Vector3.right;
        }
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        //Movement Via Character Controller
        controller.Move(motion: movement * moveSpeed * Time.deltaTime);

        //RightStick Movement
        Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            playerModel.transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            
        }
        Move(movement);
        SetSpeed();
    }


    //Movement if Player is not Aiming down Sights
    void PassiveMovement()
    {
        moveDirection = (transform.forward * v) + (transform.right * h);
        moveDirection = moveDirection.normalized * moveSpeed;

        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime);

        controller.Move(motion: moveDirection * Time.deltaTime);

        //Move the player in different directions based on camera look direction
        if (h != 0 || v != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            
        }
        SetSpeed();
    }

    //Check For Sprinting
    void IsSprinting()
    {
        if (Input.GetButtonDown("LStickDown"))
        {
            isSprinting = true;
        }
        if (Input.GetButtonUp("LStickDown"))
        {
            isSprinting = false;
        }
    }


    //ANIMATION CONTROLS
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
    }

    void SetSpeed()
    {
        anim.SetFloat("Speed", (Mathf.Abs(v)) + (Mathf.Abs(h)));
    }

    void SetMoveSpeed()
    {
        moveSpeed = (Mathf.Abs(v) + Mathf.Abs(h)) * 6;
        if (moveSpeed >= 6)
            moveSpeed = 6;
    }


}
