using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// mouse, keyboard, GUI, Men

public class UserInputBehaviour : MonoBehaviour {
    public InventoryBehaviour inventory;
    public CombatBehaviour combat;
    public MovementBehaviour movement;

    public Camera inputCamera { get; set; }

    public Vector3 inputDirection { get; private set; }
    public Vector3 inputWorldPosition { get; private set; }
    public bool isAiming { get; private set; }
    public bool isShooting { get; private set; }
    

    // UNITY UPDATE CALLS

    private void OnEnable()
    {
        inputCamera = Camera.main;
    }

    private void Update()
    {
        UpdateInputDirection();

        isAiming = Input.GetMouseButton(1);

        // responsible for calculating the way the player should be looking
        if (isAiming)
        {
            UpdateWorldPosition();
        }

        if (movement != null)
        {
            if(isAiming)movement.SetLookPoint(inputWorldPosition);
            else movement.SetLookDirection(inputDirection);

            movement.SetMoveDirection(inputDirection);
        }

        isShooting = Input.GetMouseButton(0);



        bool weaponLight = Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha1);
        bool weaponMedium = Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha2);
        bool weaponHeavy = Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha3);
        bool weaponGrenade = Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha4);

        if (inventory!=null)
        {
            if(weaponLight) inventory.SetWeapon(InventoryBehaviour.WeaponType.LightWeapon);
            if(weaponMedium) inventory.SetWeapon(InventoryBehaviour.WeaponType.MediumWeapon);
            if(weaponHeavy) inventory.SetWeapon(InventoryBehaviour.WeaponType.HeavyWeapon);
            if(weaponGrenade) inventory.SetWeapon(InventoryBehaviour.WeaponType.Grenade);
        }
    }

    // PUBLIC ACCESS

    public void DoNothing() { }

    // PRIVATE

    void UpdateInputDirection()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector3(inputX, 0f, inputY).normalized;
    }

    void UpdateWorldPosition()
    {
        // shoot a ray from cam through the mouse to the ground
        Ray ray = inputCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            inputWorldPosition = ray.GetPoint(rayDistance);
        }
    }
}
