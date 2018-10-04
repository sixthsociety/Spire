using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {
    //Variables
    public GameObject playerModel;




    //Methods
	
	
	void Update () {
        PlayerDirection();
	}

    void PlayerDirection()
    {
        transform.forward = playerModel.transform.forward;
    }
}
