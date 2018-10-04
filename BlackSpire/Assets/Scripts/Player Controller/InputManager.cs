using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    //Variables of Input
        
    //XBOX CONTROLLER    
    //Bools/Buttons
    public bool AButton;
    public bool BButton;
    public bool XButton;
    public bool YButton;
    public bool LBumper;
    public bool RBumper;
    public bool Back;
    public bool Start;
    public bool LStickButton;
    public bool RStickButton;
    //Axis' & Floats
    public float RHorizontal;
    public float RVertical;
    public float RTrigger;
    public float LTrigger;
    public float DPadHorizontal;
    public float DPadVertical;
    public float Horizontal;
    public float Vertical;

    // Update is called once per frame
    void Update () {

        AButton = (Input.GetButton("AButton"));
        BButton = (Input.GetButton("BButton"));
        XButton = (Input.GetButton("XButton"));
        YButton = (Input.GetButton("YButton"));
        LBumper = (Input.GetButton("LBumper"));
        RBumper= (Input.GetButton("RBumper"));
        Back = (Input.GetButton("Back"));
        Start = (Input.GetButton("Start"));
        LStickButton = (Input.GetButton("LStickButton"));
        RStickButton = (Input.GetButton("RStickButton"));
        //Axis' & Floats
        RHorizontal = (Input.GetAxis("RHorizontal"));
        RVertical = (Input.GetAxis("RVertical"));
        RTrigger = (Input.GetAxis("RTrigger"));
        LTrigger = (Input.GetAxis("LTrigger"));
        DPadHorizontal = (Input.GetAxis("DPadHorizontal"));
        DPadVertical = (Input.GetAxis("DPadVertical"));
        Vertical = (Input.GetAxis("Vertical"));
        Horizontal = (Input.GetAxis("Horizontal"));

}
}
