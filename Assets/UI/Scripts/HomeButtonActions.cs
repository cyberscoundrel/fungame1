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

    public void RoomPanelCloseOnclick()
    { 
    
    }

    public void RoomPanelSubmtiOnclick()
    { 
    
    }

}
