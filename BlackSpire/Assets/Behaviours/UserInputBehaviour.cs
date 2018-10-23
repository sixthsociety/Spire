using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// mouse, keyboard, GUI, Men

public class UserInputBehaviour : MonoBehaviour {

    public Camera inputCamera { get; set; }

    public Vector3 inputDirection { get; private set; }
    public Vector3 inputWorldPosition { get; private set; }
    public bool isAiming { get; private set; }

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
