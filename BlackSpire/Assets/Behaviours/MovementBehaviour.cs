using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// walk, run, roll, teleport, environmental factors, throwable

public class MovementBehaviour : MonoBehaviour {

    //[SerializeField]
    // WILL SERIALIZE LATER
    float m_DefaultSpeed = 5f;
    float maxRotateSpeed = 1000f;

    public float speed { get; set; }

    public Vector3 moveDirection { get; private set; }
    public Vector3 lookDirection { get; private set; }
    public Vector3 lookPoint { get; private set; }
    public bool isAiming { get; private set; }

    public Quaternion lookRotation { get; private set; }
    public Vector3 tagrgetLookDirection { get; private set; }

    new private Rigidbody rigidbody;
    public CapsuleCollider capsuleCollider;

    // UNITY UPDATE CALLS

    void OnEnable()
    {
        InitializeRigidbody();

        speed = m_DefaultSpeed;

        tagrgetLookDirection = transform.forward; //Vector3.forward;
        lookRotation = transform.rotation; //Quaternion.identity;
    }

    void FixedUpdate()
    {
        if (isAiming)
        {
            Look(lookPoint - transform.position, Time.fixedDeltaTime);
            Move(moveDirection, Time.fixedDeltaTime);
        }
        else
        {
            Look(lookDirection, Time.fixedDeltaTime);
            Move(moveDirection, Time.fixedDeltaTime);
        }
    }

    // PUBLIC ACCESS

    public void SetLookPoint(Vector3 point)
    {
        isAiming = true;
        lookPoint = point;
    }

    public void SetLookDirection(Vector3 direction)
    {
        isAiming = false;
        lookDirection = direction;
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    // PRIVATE

    void Move(Vector3 direction, float deltaTime)
    {
        rigidbody.MovePosition(rigidbody.position + direction * deltaTime * speed);
        /*
        //Animation
        Vector3 localDirection = Quaternion.Inverse(lookRotation) * direction;
        animator.SetFloat("MoveX", localDirection.x);
        animator.SetFloat("MoveY", localDirection.z);
        */
    }

    void Look(Vector3 direction, float deltaTime)
    {
        direction.y = 0f;

        if (direction.sqrMagnitude > Mathf.Epsilon)
        {
            tagrgetLookDirection = direction;
        }

        Quaternion targetLookRotation = Quaternion.LookRotation(tagrgetLookDirection, Vector3.up);
        lookRotation = Quaternion.RotateTowards(lookRotation, targetLookRotation, maxRotateSpeed * deltaTime);
        transform.rotation = lookRotation;
    }

    void InitializeRigidbody()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody == null) rigidbody = this.gameObject.AddComponent<Rigidbody>();

        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    bool CanWarp(Vector3 position)
    {
        //Debug.Log("Check area");
        if (capsuleCollider == null) throw new System.Exception("No capsule attached");

        Collider[] colliders =
            Physics.OverlapCapsule(position, position + Vector3.up * capsuleCollider.height,
            capsuleCollider.radius, -1, QueryTriggerInteraction.Ignore);

        return colliders.Length == 0;
    }

    void Warp(Vector3 position)
    {
        transform.position = position;
        Debug.Log("Player warped");
    }
}
