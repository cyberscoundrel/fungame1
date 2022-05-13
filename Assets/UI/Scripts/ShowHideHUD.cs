using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideHUD : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hud;

    void Start()
    {
        hud.GetComponent<CanvasGroup>().alpha = 0;
        hud.GetComponent<CanvasGroup>().interactable = false;
        hud.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    public void hideHUD() {
        hud.GetComponent<CanvasGroup>().alpha = 0;
        hud.GetComponent<CanvasGroup>().interactable = false;
        hud.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void showHUD()
    {
        Debug.Log("ShowHud");
        hud.GetComponent<CanvasGroup>().alpha = 1;
        hud.GetComponent<CanvasGroup>().interactable = true;
        hud.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
