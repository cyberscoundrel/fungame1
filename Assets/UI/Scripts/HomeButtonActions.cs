using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    public void RoomPanelCloseOnclick()
    {
        GameObject.Find("HomeCanvas").GetComponent<HomeManager>().toggleRoomCodeDialog();
    }

    public void RoomPanelSubmitOnclick()
    {
        //GameObject.Find("MainMenuUI").SetActive(false);
        GameObject.Find("CliManager").SetActive(true);
    }

}
