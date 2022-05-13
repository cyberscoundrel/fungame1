using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool pauseOpen = false;
    private static bool isTweening = false;
    public LeanTweenType easing;
    public float transitionSpeed;

    void Start()
    {
        CanvasGroup pausePanel = GameObject.Find("PausePanel").GetComponent<CanvasGroup>();
        CanvasGroup pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<CanvasGroup>();
        pausePanel.alpha = 0;
        pausePanel.interactable = false;
        pausePanel.blocksRaycasts = false;
        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") && !isTweening)
        {
            print("escape key was pressed");
            if (!pauseOpen)
            {
                openPauseMenu();
            }
            else {
                closePauseMenu();
            }
        }
    }


    public static bool isPauseOpen()
    {
        return pauseOpen;
    }

    public void openPauseMenu()
    {
        if (!pauseOpen)
        {
            isTweening = true;
            GameObject pausePanel = GameObject.Find("PausePanel");
            GameObject pauseCanvas = GameObject.Find("PauseCanvas");
            pausePanel.GetComponent<CanvasGroup>().interactable = true;
            pausePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            pauseCanvas.GetComponent<CanvasGroup>().interactable = true;
            pauseCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
            LeanTween.moveY(pausePanel.GetComponent<RectTransform>(), pausePanel.GetComponent<RectTransform>().anchoredPosition.y + 40f, transitionSpeed).setFrom(pausePanel.GetComponent<RectTransform>().anchoredPosition.y).setEase(easing);
            LeanTween.value(pauseCanvas, 0, 1, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                pauseCanvas.GetComponent<CanvasGroup>().alpha = val;
            });
            LeanTween.value(pausePanel, 0, 1, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                pausePanel.GetComponent<CanvasGroup>().alpha = val;

            }).setOnComplete(() =>
            {
                pausePanel.GetComponent<CanvasGroup>().alpha = 1;
                pauseOpen = true;
                isTweening = false;
            });
        }
    }

    public void closePauseMenu() {
        if (pauseOpen) {
            isTweening = true;
            GameObject pausePanel = GameObject.Find("PausePanel");
            GameObject pauseCanvas = GameObject.Find("PauseCanvas");
            pausePanel.GetComponent<CanvasGroup>().interactable = false;
            pausePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            pauseCanvas.GetComponent<CanvasGroup>().interactable = false;
            pauseCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
            LeanTween.moveY(pausePanel.GetComponent<RectTransform>(), pausePanel.GetComponent<RectTransform>().anchoredPosition.y - 40f, transitionSpeed).setFrom(pausePanel.GetComponent<RectTransform>().anchoredPosition.y).setEase(easing);
            LeanTween.value(pauseCanvas, 1, 0, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                pauseCanvas.GetComponent<CanvasGroup>().alpha = val;
            });
            LeanTween.value(pausePanel, 1, 0, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                pausePanel.GetComponent<CanvasGroup>().alpha = val;

            }).setOnComplete(() => 
            {
                pausePanel.GetComponent<CanvasGroup>().alpha = 0;
                pauseOpen = false;
                isTweening = false;
            });
        }
    }
}
