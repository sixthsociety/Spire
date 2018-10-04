using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.MonoBehaviour {

    public GameObject[] redSpawns;
    public GameObject[] blueSpawns;

    void Connect()
    {
        PhotonNetwork.ConnectToBestCloudServer("V1.0");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
