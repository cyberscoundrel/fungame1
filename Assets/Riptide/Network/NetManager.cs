using RiptideNetworking;
using RiptideNetworking.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerToClientId : ushort
{
	playerSpawned = 2,
	playerMovement = 5,
	playerPickedUp = 8,
	playerDropped = 10,
	playerShoot = 12,
	enemyShoot = 14,
	entityShoot = 111,
	enemyPickedUp = 16,
	entityPickedUp = 115,
	enemyDropped = 18,
	entityDropped = 117,
	playerHit = 121,
	playerDamage = 20,
	enemyHit = 125,
	entityHit = 101,
	enemyDamage = 22,
	entityDamage = 103,
	enemyMovement = 24,
	entityMovement = 107,
	collectibleSpawned = 26,
	collectibleMovement = 28,
	changePlanet = 1001,
	
}

public enum ClientToServerId : ushort
{
	name = 1,
	move = 3,
	pickup = 7,
	drop = 9,
	shoot = 13,
	damage = 21,
	hitEnemy = 515,
	hurt = 517,
	//TODO independent physics
	collectibleMove = 99,
	enemyMove = 211,

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

    	Player newPlayer = PlayerManager.instance.createNewPlayer<LocalPlayer>(PlayerManager.instance.localguy);
    	newPlayer.uTag = 0;
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
