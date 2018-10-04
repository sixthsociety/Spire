using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForSprint : MonoBehaviour {

    public bool isSprinting;
    public float moveSpeed;

	// Use this for initialization
	void Start () {
        isSprinting = false;
        moveSpeed = 1;
	}
	
	// Update is called once per frame
	void Update () {
        IsSprinting();

        if (isSprinting)
        {
            moveSpeed = 12;
        }
        if (!isSprinting)
        {
            moveSpeed = 6;
        }
	}

    //IsSprinting Checl
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
}
