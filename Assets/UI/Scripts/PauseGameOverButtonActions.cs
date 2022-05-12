using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameOverButtonActions : MonoBehaviour
{
    // Start is called before the first frame update
    public void resumeOnclick() 
    {
        GameObject.Find("PauseCanvas").GetComponent<PauseMenuManager>().closePauseMenu();
        Debug.Log("Resume");
    }

    public void exitOnclick()
    {
        Debug.Log("Exit");
    }

    public void newgameOnclick()
    { 
    }
}
