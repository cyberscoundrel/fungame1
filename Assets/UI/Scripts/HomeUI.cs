using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HomeUI : MonoBehaviour
{
    
    
    public LeanTweenType easing;
    public float animationSpeed;
    static public bool multiplayerOpen = false;
    public float transitionSpeed;
    // Start is called before the first frame update
    public void Start() {
        CanvasGroup onlineText = GameObject.Find("OnlineText").GetComponent<CanvasGroup>();
        CanvasGroup multiPanel = GameObject.Find("MultiPanel").GetComponent<CanvasGroup>();
        onlineText.alpha = 0;
        onlineText.interactable = false;
        onlineText.blocksRaycasts = false;
        multiPanel.alpha = 0;
        multiPanel.interactable = false;
        multiPanel.blocksRaycasts = false;
    }

    public void Awake() {
        multiplayerOpen = false;
    }

    public void scaleMultiOnHover()
    {
        if (multiplayerOpen == false)
        {
            LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), animationSpeed).setEase(easing);
        }
    }

    public void scaleMultiOnUnHover()
    {
        if (multiplayerOpen == false)
        {
            LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), animationSpeed).setEase(easing);
        }
     
    }

    public void scaleUpOnHover() 
    {
        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), animationSpeed).setEase(easing);
    }

    public void scaleDownOnUnhover()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), animationSpeed).setEase(easing);
    }

    public void multiplayerOpenOnclick() {
        Debug.Log("Main onlick: " + multiplayerOpen);
        if (multiplayerOpen == false)
        {
            GameObject playText = GameObject.Find("PlayText");
            GameObject onlineText = GameObject.Find("OnlineText");
            GameObject mainPanel = GameObject.Find("MainPanel");
            GameObject multiPanel = GameObject.Find("MultiPanel");

            LeanTween.moveY(mainPanel.GetComponent<RectTransform>(), mainPanel.GetComponent<RectTransform>().anchoredPosition.y - 40f, transitionSpeed).setEase(easing);
            //LeanTween.moveX(multiPanel.GetComponent<RectTransform>(), multiPanel.GetComponent<RectTransform>().anchoredPosition.x + 40f, animationSpeed).setEase(easing);

            LeanTween.value(mainPanel, 1, 0, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                mainPanel.GetComponent<CanvasGroup>().alpha = val;

            });
            LeanTween.value(playText, 1, 0, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                playText.GetComponent<CanvasGroup>().alpha = val;
                onlineText.GetComponent<CanvasGroup>().alpha = 1 - val;
            });
            LeanTween.value(multiPanel, 0, 1, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                multiPanel.GetComponent<CanvasGroup>().alpha = val;

            }).setOnComplete(hideMainMenu);
        }

        
    }

    public void mainmenuOpenOnclick()
    {
        Debug.Log("Main onlick: " + multiplayerOpen);
        if (multiplayerOpen)
        {
            GameObject playText = GameObject.Find("PlayText");
            GameObject onlineText = GameObject.Find("OnlineText");
            GameObject mainPanel = GameObject.Find("MainPanel");
            GameObject multiPanel = GameObject.Find("MultiPanel");

            LeanTween.moveY(multiPanel.GetComponent<RectTransform>(), multiPanel.GetComponent<RectTransform>().anchoredPosition.y - 40f, transitionSpeed).setEase(easing);
            //LeanTween.moveX(multiPanel.GetComponent<RectTransform>(), multiPanel.GetComponent<RectTransform>().anchoredPosition.x + 40f, animationSpeed).setEase(easing);

            LeanTween.value(multiPanel, 1, 0, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                multiPanel.GetComponent<CanvasGroup>().alpha = val;

            });
            LeanTween.value(onlineText, 1, 0, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                onlineText.GetComponent<CanvasGroup>().alpha = val;
                playText.GetComponent<CanvasGroup>().alpha = 1 - val;
            });
            LeanTween.value(mainPanel, 0, 1, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                mainPanel.GetComponent<CanvasGroup>().alpha = val;

            }).setOnComplete(showMainMenu);
            //multiplayerOpen = false;
        }
    }


    public void hideMainMenu()
    {
        CanvasGroup mainPanel = GameObject.Find("MainPanel").GetComponent<CanvasGroup>();
        CanvasGroup multiPanel = GameObject.Find("MultiPanel").GetComponent<CanvasGroup>();
        LeanTween.moveY(mainPanel.GetComponent<RectTransform>(), mainPanel.GetComponent<RectTransform>().anchoredPosition.y + 40f, transitionSpeed).setEase(easing);
        mainPanel.alpha = 0;
        mainPanel.interactable = false;
        mainPanel.blocksRaycasts = false;
        multiPanel.alpha = 1;
        multiPanel.interactable = true;
        multiPanel.blocksRaycasts = true;
        multiplayerOpen = true;
        Debug.Log("multiplayerOpen " + multiplayerOpen);
        Debug.Log("HIDE");
    }

    public void showMainMenu()
    {
        CanvasGroup mainPanel = GameObject.Find("MainPanel").GetComponent<CanvasGroup>();
        CanvasGroup multiPanel = GameObject.Find("MultiPanel").GetComponent<CanvasGroup>();
        LeanTween.moveY(multiPanel.GetComponent<RectTransform>(), multiPanel.GetComponent<RectTransform>().anchoredPosition.y + 40f, transitionSpeed).setEase(easing);
        multiPanel.alpha = 0;
        multiPanel.interactable = false;
        multiPanel.blocksRaycasts = false;
        mainPanel.alpha = 1;
        mainPanel.interactable = true;
        mainPanel.blocksRaycasts = true;
        multiplayerOpen = false;
        Debug.Log("multiplayerOpen " + multiplayerOpen);
        Debug.Log("SHOW");
    }




}
