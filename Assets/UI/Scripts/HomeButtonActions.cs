using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HomeButtonActions : MonoBehaviour
{
    public void SingleplayerOnclick()
    {

    }

    public void OnlineOnclick()
    {
        Debug.Log("Online Clicked");
        GameObject.Find("HomeCanvas").GetComponent<HomeManager>().swapMainWithMulti();
    }

    public void CreateOnclick()
    { 
        Debug.Log("CreateOnclick");
        GameObject.Find("NetManager").GetComponent<NetManager>().enabled = true;
        GameObject.Find("MainMenuUI").SetActive(false);
        ControlledObject.instance.planetWatch = false;
        GalaxyManager.enterGame();
    
    }

    public void JoinOnclick()
    {
        Debug.Log("Join Clicked");
        GameObject.Find("HomeCanvas").GetComponent<HomeManager>().toggleRoomCodeDialog();
    }

    public void BackOnclick()
    {
        GameObject.Find("HomeCanvas").GetComponent<HomeManager>().swapMultiWithMain();
    }

    public void RoomPanelCheckboxOnclick() {
        if (this.GetComponent<Toggle>().isOn)
        {
            Debug.Log(this.GetComponent<Toggle>().isOn);
            GameObject.Find("LobbyCodeInputTextArea").transform.Find("Placeholder").GetComponent<TextMeshProUGUI>().SetText("IP Address");
        }
        else {
            GameObject.Find("LobbyCodeInputTextArea").transform.Find("Placeholder").GetComponent<TextMeshProUGUI>().SetText("Room Code");
        }
    }

    public void RoomPanelCloseOnclick()
    {
        GameObject.Find("HomeCanvas").GetComponent<HomeManager>().toggleRoomCodeDialog();
    }

    public void RoomPanelSubmitOnclick()
    {
        //GameObject.Find("MainMenuUI").SetActive(false);
        //GameObject.Find("CliManager").SetActive(true);
        GameObject.Find("CliManager").GetComponent<CliManager>().enabled = true;
    }

}
