using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintController : MonoBehaviour {

    InputManager inputManager;
    PMController passiveMove;

    //Variables for PM CONTROLLER
    public Rigidbody rigidBody;
    public float rotateSpeed = 6f;
    private Vector3 moveDirection;
    private float moveSpeed;
    public float sprintValue;
    public bool IsAiming;
    public Transform cam;
    public GameObject playerModel, pivot;
    public Animator anim;

    // Use this for initialization
    void Start () {
        pivot = GameObject.Find("Pivot");
        inputManager = GetComponent<InputManager>();
        passiveMove = GetComponent<PMController>();
        rigidBody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        sprintValue = 20f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        PassiveMovement();
        SetMoveSpeed();
        SetSpeed();
        UpdateAnimator();
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
            Debug.Log("Rotation" + playerModel.transform.forward);

        }
    }

    void SetMoveSpeed()
    {
        moveSpeed = (Mathf.Abs(inputManager.Vertical) + Mathf.Abs(inputManager.Horizontal)) * sprintValue;
        if (moveSpeed >= sprintValue)
            moveSpeed = sprintValue;
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
