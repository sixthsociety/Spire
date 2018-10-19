using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour {

    public Transform menu;

    private void Update() {
        if (!menu.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
            menu.gameObject.SetActive(true);
        }
        else if(menu.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
            menu.gameObject.SetActive(false);
        }
    }

    public void BackButton() {
        menu.gameObject.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
