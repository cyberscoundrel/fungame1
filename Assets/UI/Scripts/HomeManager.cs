using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool multiplayerOpen = false;
    private static bool roomCodeOpen = false;
    public LeanTweenType easing;
    public float transitionSpeed;

    void Start()
    {
        CanvasGroup mulitplayerSubtitle = GameObject.Find("MultiplayerText").GetComponent<CanvasGroup>();
        CanvasGroup multiPanel = GameObject.Find("MultiPanel").GetComponent<CanvasGroup>();
        CanvasGroup roomPanel = GameObject.Find("RoomCodePanel").GetComponent<CanvasGroup>();
        mulitplayerSubtitle.alpha = 0;
        mulitplayerSubtitle.interactable = false;
        mulitplayerSubtitle.blocksRaycasts = false;
        multiPanel.alpha = 0;
        multiPanel.interactable = false;
        multiPanel.blocksRaycasts = false;
        roomPanel.alpha = 0;
        roomPanel.interactable = false;
        roomPanel.blocksRaycasts = false;

        ControlledObject.instance.locked = false;
        ControlledObject.instance.firstPerson = false;
        ControlledObject.instance.topDown = false;
        ControlledObject.instance.planetWatch = true;
        //ControlledObject.instance.topDown = true;
        ControlledObject.setControlledObject(GalaxyManager.instance.startPlanet.gameObject);
    }

    public static bool isMultiplayerPanelOpen() 
    {
        return multiplayerOpen;
    }

    public static bool isRoomCodePanelOpen()
    {
        return roomCodeOpen;
    }

    public void swapMainWithMulti() 
    {
        Debug.Log("Swap Main with Multi");
        if (!multiplayerOpen)
        {
            GameObject playText = GameObject.Find("PlayText");
            GameObject onlineText = GameObject.Find("MultiplayerText");
            GameObject mainPanel = GameObject.Find("MainPanel");
            GameObject multiPanel = GameObject.Find("MultiPanel");
            mainPanel.GetComponent<CanvasGroup>().interactable = false;
            mainPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            LeanTween.moveY(mainPanel.GetComponent<RectTransform>(), mainPanel.GetComponent<RectTransform>().anchoredPosition.y - 40f, transitionSpeed).setEase(easing);
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

            }).setOnComplete(() =>
            {
                LeanTween.moveY(mainPanel.GetComponent<RectTransform>(), mainPanel.GetComponent<RectTransform>().anchoredPosition.y + 40f, transitionSpeed).setEase(easing);
                mainPanel.GetComponent<CanvasGroup>().alpha = 0;
                multiPanel.GetComponent<CanvasGroup>().alpha = 1;
                multiPanel.GetComponent<CanvasGroup>().interactable = true;
                multiPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
                multiplayerOpen = true;
            });
        }
    }

    public void swapMultiWithMain() 
    {
        if (roomCodeOpen) {
            toggleRoomCodeDialog();
        }
        if (multiplayerOpen) {
            GameObject playText = GameObject.Find("PlayText");
            GameObject onlineText = GameObject.Find("MultiplayerText");
            GameObject mainPanel = GameObject.Find("MainPanel");
            GameObject multiPanel = GameObject.Find("MultiPanel");
            multiPanel.GetComponent<CanvasGroup>().interactable = false;
            multiPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            LeanTween.moveY(multiPanel.GetComponent<RectTransform>(), multiPanel.GetComponent<RectTransform>().anchoredPosition.y - 40f, transitionSpeed).setEase(easing);
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

            }).setOnComplete(() =>
            {
                LeanTween.moveY(multiPanel.GetComponent<RectTransform>(), multiPanel.GetComponent<RectTransform>().anchoredPosition.y + 40f, transitionSpeed).setEase(easing);
                multiPanel.GetComponent<CanvasGroup>().alpha = 0;
                mainPanel.GetComponent<CanvasGroup>().alpha = 1;
                mainPanel.GetComponent<CanvasGroup>().interactable = true;
                mainPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
                multiplayerOpen = false;
            });
        }
    }

    public void toggleRoomCodeDialog()
    {
        if (!roomCodeOpen)
        {
            GameObject roomPanel = GameObject.Find("RoomCodePanel");
            GameObject joinButton = GameObject.Find("JoinButton");
            joinButton.GetComponent<Button>().interactable = false;
            LeanTween.moveX(roomPanel.GetComponent<RectTransform>(), roomPanel.GetComponent<RectTransform>().anchoredPosition.x - 40f, transitionSpeed).setEase(easing);
            LeanTween.value(roomPanel, 0, 1, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                roomPanel.GetComponent<CanvasGroup>().alpha = val;
            }).setOnComplete(() =>
            {
                CanvasGroup roomCodeCanvas = roomPanel.GetComponent<CanvasGroup>();
                roomCodeCanvas.alpha = 1;
                roomCodeCanvas.interactable = true;
                roomCodeCanvas.blocksRaycasts = true;
                roomCodeOpen = true;
                joinButton.GetComponent<Button>().interactable = true;
            });
        }
        else 
        {
            GameObject roomPanel = GameObject.Find("RoomCodePanel");
            GameObject joinButton = GameObject.Find("JoinButton");
            joinButton.GetComponent<Button>().interactable = false;
            LeanTween.moveX(roomPanel.GetComponent<RectTransform>(), roomPanel.GetComponent<RectTransform>().anchoredPosition.x + 40f, transitionSpeed).setEase(easing);
            LeanTween.value(roomPanel, 1, 0, transitionSpeed).setEase(easing).setOnUpdate((float val) =>
            {
                roomPanel.GetComponent<CanvasGroup>().alpha = val;
            }).setOnComplete(() =>
            {
                CanvasGroup roomCodeCanvas = roomPanel.GetComponent<CanvasGroup>();
                roomCodeCanvas.alpha = 0;
                roomCodeCanvas.interactable = false;
                roomCodeCanvas.blocksRaycasts = false;
                roomCodeOpen = false;
                joinButton.GetComponent<Button>().interactable = true;
            });
        }

    }
}
