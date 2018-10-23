using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// walk, run, roll, teleport, environmental factors, throwable

public class MovementBehaviour : MonoBehaviour {

    [SerializeField] float m_DefaultSpeed = 1f;
        
    public float speed { get; set; }
    private Vector3 lookDirection { get; set; }
    private Vector3 lookPoint { get; set; }
    private bool isAiming { get; set; }

    public Quaternion lookRotation { get; private set; }

    new private Rigidbody rigidbody;
    
    // UNITY UPDATE CALLS

    void OnEnable()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        lookRotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        if (isAiming)
        {
            Look(lookPoint - transform.position);
            Move(lookDirection);
        }
        else
        {
            Look(lookDirection);
            Move(lookDirection);
        }
    }

    // PUBLIC ACCESS

    public void SetLookPoint() { throw new System.Exception("unimplemented, set lookPosition instead"); }

    public void SetLookDirection() { throw new System.Exception("unimplemented, set lookDirection instead"); }

    // PRIVATE

    void Move(Vector3 direction)
    {
        rigidbody.MovePosition(rigidbody.position + direction * speed);
        /*
        //Animation
        Vector3 localDirection = Quaternion.Inverse(lookRotation) * direction;
        animator.SetFloat("MoveX", localDirection.x);
        animator.SetFloat("MoveY", localDirection.z);
        */
    }

    void Look(Vector3 direction)
    {
        if (direction.sqrMagnitude < Mathf.Epsilon) return;
        direction.y = 0f;
        lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = lookRotation;
    }
}
