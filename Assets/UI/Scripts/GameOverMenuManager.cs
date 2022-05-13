using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool gameOverOpen = false;
    private static bool isTweening = false;
    public LeanTweenType easing;
    public float transitionSpeed;

    void Start()
    {
        CanvasGroup pausePanel = GameObject.Find("PausePanel").GetComponent<CanvasGroup>();
        CanvasGroup pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<CanvasGroup>();
        /*pausePanel.alpha = 0;
        pausePanel.interactable = false;
        pausePanel.blocksRaycasts = false;
        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;*/
    }

    void Update()
    {
        if (Input.GetKeyDown("f1") && !isTweening)
        {
            print("f1 key was pressed");
            if (!gameOverOpen)
            {
                openGameOverMenu();
            }
            else {
                closeGameOverMenu();
            }
        }
    }


    public static bool isGameOverOpen()
    {
        return gameOverOpen;
    }

    public void openGameOverMenu()
    {
        if (!gameOverOpen)
        {
            isTweening = true;
            GameObject gameoverPanel = GameObject.Find("GameOverPanel");
            GameObject gameoverCanvas = GameObject.Find("GameOverCanvas");
            gameoverPanel.GetComponent<CanvasGroup>().interactable = true;
            gameoverPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            gameoverCanvas.GetComponent<CanvasGroup>().interactable = true;
            gameoverCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
            LeanTween.moveY(gameoverPanel.GetComponent<RectTransform>(), gameoverPanel.GetComponent<RectTransform>().anchoredPosition.y + 40f, transitionSpeed).setFrom(gameoverPanel.GetComponent<RectTransform>().anchoredPosition.y).setEase(easing);
            LeanTween.value(gameoverCanvas, 0, 1, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                gameoverCanvas.GetComponent<CanvasGroup>().alpha = val;
            });
            LeanTween.value(gameoverPanel, 0, 1, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                gameoverPanel.GetComponent<CanvasGroup>().alpha = val;

            }).setOnComplete(() =>
            {
                gameoverPanel.GetComponent<CanvasGroup>().alpha = 1;
                gameOverOpen = true;
                isTweening = false;
            });
        }
    }

    public void closeGameOverMenu() {
        if (gameOverOpen) {
            isTweening = true;
            GameObject gameoverPanel = GameObject.Find("GameOverPanel");
            GameObject gameoverCanvas = GameObject.Find("GameOverCanvas");
            gameoverPanel.GetComponent<CanvasGroup>().interactable = false;
            gameoverPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            gameoverCanvas.GetComponent<CanvasGroup>().interactable = false;
            gameoverCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
            LeanTween.moveY(gameoverPanel.GetComponent<RectTransform>(), gameoverPanel.GetComponent<RectTransform>().anchoredPosition.y - 40f, transitionSpeed).setFrom(gameoverPanel.GetComponent<RectTransform>().anchoredPosition.y).setEase(easing);
            LeanTween.value(gameoverCanvas, 1, 0, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                gameoverCanvas.GetComponent<CanvasGroup>().alpha = val;
            });
            LeanTween.value(gameoverPanel, 1, 0, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                gameoverPanel.GetComponent<CanvasGroup>().alpha = val;

            }).setOnComplete(() => 
            {
                gameoverPanel.GetComponent<CanvasGroup>().alpha = 0;
                gameOverOpen = false;
                isTweening = false;
            });
        }
    }
}
