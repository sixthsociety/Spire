using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NPCgui : MonoBehaviour
{
    public Button interactionButton;

    public void DisplayInteractionButton()
    {
        interactionButton.gameObject.SetActive(true);
        Debug.Log(interactionButton.IsActive());
    }

    public void HideInteractionButton() 
    {
        interactionButton.gameObject.SetActive(false);
    }
}
