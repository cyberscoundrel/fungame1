using RiptideNetworking;
using RiptideNetworking.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum ServerToClientId : ushort
{
	playerSpawned = 2
}

public enum ClientToServerId : ushort
{
	name = 1
}*/


public class CliManager : MonoBehaviour
{
	public static Client client;

	[SerializeField] public String ip;
	[SerializeField] public ushort port;

	public static CliManager instance;

    // Start is called before the first frame update
    void Start()
    {
    	instance = this;
    	RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
    	client = new Client();
    	client.Connected += DidConnect;
    	client.Connect($"{ip}:{port}");
    	//client.ClientConnected += DidConnect;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	client.Tick();


        
    }

    void OnApplicationQuit()
    {
    	client.Disconnect();
    }

    public void DidConnect(object sender, EventArgs e)
    {
    	Debug.Log("oober doober");
    	Message m = Message.Create(MessageSendMode.reliable, (ushort)ClientToServerId.name);
    	m.AddString("oober doober");
    	client.Send(m);
        GameObject.Find("MainMenuUI").SetActive(false);
        //GameObject.Find("GalaxyManager").SetActive(true);
        GameObject.Find("PlayerManager").SetActive(true);
        GameObject.Find("EnemyManager").SetActive(true);
        GameObject.Find("CollectibleManager").SetActive(true);
    }
}
