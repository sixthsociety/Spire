using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for interaction between npc and player
[RequireComponent(typeof(NPCgui))]
public class InteractableNPCBehaviour : MonoBehaviour {

    public bool m_IsEnabled = true;  //is the player allowed to interact with this player

    NPCgui gui;

    private void Start()
    {
        gui = GetComponent<NPCgui>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") 
        {
            gui.DisplayInteractionButton();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            gui.HideInteractionButton();
        }
    }
}
