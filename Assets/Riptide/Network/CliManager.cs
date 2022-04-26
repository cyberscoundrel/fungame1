using RiptideNetworking;
using RiptideNetworking.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliManager : MonoBehaviour
{

	public enum ServerToClientId : ushort
	{
		playerConnect = 1
	}

	public enum ClientToServerId : ushort
	{
		name = 1
	}

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
    	client.Connect($"{ip}:{port}");
        
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
    	Message m = Message.Create(MessageSendMode.reliable, (ushort)ClientToServerId.name);
    	m.AddString("oober doober");
    	client.Send(m);

    }
}
