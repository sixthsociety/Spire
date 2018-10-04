using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {

    //Variables
    public bool IsSprinting;
    public bool IsAiming;
    public AimController AimController;
    public PMController PMController;
    public InputManager inputManager;
    public SprintController sprintController;

    //Methods
    private void Start()
    {
        AimController = GetComponent<AimController>();
        PMController = GetComponent<PMController>();
        inputManager = GetComponent<InputManager>();
        sprintController = GetComponent<SprintController>();
    }


    void Update () {
        //Check for action
        Aiming();
        Sprinting();

        if (IsAiming)
        {
            AimController.enabled = true;
            PMController.enabled = false;
            sprintController.enabled = false;
            IsSprinting = false;
        }
        if (!IsAiming && !IsSprinting)
        {
            PMController.enabled = true;
            AimController.enabled = false;
            sprintController.enabled = false;
        }
        if (!IsAiming && IsSprinting)
        {
            PMController.enabled = false;
            AimController.enabled = false;
            sprintController.enabled = true;
        }

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

    void Sprinting()
    {
        if (Input.GetButtonDown("LStickButton"))
        {
            if (IsSprinting == false)
            {
                IsSprinting = true;
            }
            else if (IsSprinting == true)
            {
                IsSprinting = false;
            }

        }
    }

}
