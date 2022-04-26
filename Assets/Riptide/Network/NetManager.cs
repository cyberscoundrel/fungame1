using RiptideNetworking;
using RiptideNetworking.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerToClientId : ushort
{
	playerSpawned = 2,
	playerMovement = 5,
}

public enum ClientToServerId : ushort
{
	name = 1,
	move = 3,
}

public class NetManager : MonoBehaviour
{

	public Server server;

	public ushort port;
	public ushort maxClientCount;

	public static NetManager instance;
    // Start is called before the first frame update
    void Start()
    {

    	RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

    	server = new Server();
    	server.Start(port, maxClientCount);
    	server.ClientDisconnected += PlayerLeft;

    	PlayerManager.instance.createNewPlayer<LocalPlayer>(PlayerManager.instance.localguy);
    	PlayerManager.instance.setPlayerOne(0);

    	instance = this;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	server.Tick();
        
    }

    void OnApplicationQuit()
    {
    	server.Stop();
    }



    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
    	//remove player here
    }


}