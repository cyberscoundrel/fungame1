using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	private ConnectionManager cManager;

	private MessageQueue msgQueue;

	private IEnumerator locationUpdate()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.2f);
			Debug.Log("sending from player " + PlayerManager.instance.player1.getUTag() + " position"  + PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.transform.position);
			bool worked = SendChangePositionRequest(PlayerManager.instance.player1.getUTag(),
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.transform.position,
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.velocity,
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.rotation, 
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.transform.position,
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.velocity,
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.rotation);
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);

		gameObject.AddComponent<MessageQueue>();
		gameObject.AddComponent<ConnectionManager>();

		NetworkRequestTable.init();
		NetworkResponseTable.init();
	}

	// Start is called before the first frame update
	void Start()
    {
		cManager = GetComponent<ConnectionManager>();

		if (cManager)
		{
			cManager.setupSocket();

			StartCoroutine(RequestHeartbeat(0.1f));
			{
				msgQueue = GetComponent<MessageQueue>();
				msgQueue.AddCallback(Constants.SMSG_JOIN, OnResponseJoin);
				msgQueue.AddCallback(Constants.SMSG_CHANGEPOSITION, OnResponseChangePosition);
				Debug.Log("Send JoinReq");
				bool connected = SendJoinRequest();
				if (!connected)
				{
					Debug.Log("unable to connect to server");
					//messageBoxMsg.text = "Unable to connect to server.";
					//messageBox.SetActive(true);
				}
			}
			StartCoroutine(locationUpdate());
		}
	}

	void FixedUpdate()
	{
		/*Debug.Log("sending from player " + PlayerManager.instance.player1.getUTag() + " position"  + PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.transform.position);
		bool worked = SendChangePositionRequest(PlayerManager.instance.player1.getUTag(),
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.transform.position,
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.velocity,
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.rotation, 
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.transform.position,
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.velocity,
			PlayerManager.instance.player1.gameObject.GetComponent<RagdollScript>().head.rotation);*/
		//Debug.Log("worked " + worked);


	}

	public bool SendJoinRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestJoin request = new RequestJoin();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendLeaveRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestLeave request = new RequestLeave();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendSetNameRequest(string Name)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestSetName request = new RequestSetName();
			request.send(Name);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendReadyRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestReady request = new RequestReady();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendMoveRequest(int pieceIndex, int x, int y)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestMove request = new RequestMove();
			request.send(pieceIndex, x, y);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendChangePositionRequest(int playerId, Vector3 playerPosition, Vector3 playerVelocity, Quaternion playerRotation, Vector3 weaponPosition, Vector3 weaponVelocity, Quaternion weaponRotation)
	{
		if(cManager && cManager.IsConnected())
		{
			RequestChangePosition request = new RequestChangePosition();
			request.send(playerId, playerPosition, playerVelocity, playerRotation, weaponPosition, weaponVelocity, weaponRotation);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendInteractRequest(int pieceIndex, int targetIndex)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestInteract request = new RequestInteract();
			request.send(pieceIndex, targetIndex);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public IEnumerator RequestHeartbeat(float time)
	{
		yield return new WaitForSeconds(time);

		if (cManager)
		{
			RequestHeartbeat request = new RequestHeartbeat();
			request.send();
			cManager.send(request);
		}

		StartCoroutine(RequestHeartbeat(time));
	}

	public void OnResponseChangePosition(ExtendedEventArgs eventArgs)
	{
		ResponseChangePositionArgs args = eventArgs as ResponseChangePositionArgs;
		if(args.player_id != PlayerManager.instance.player1.getUTag())
		{
			Debug.Log("hello?");
			Debug.Log("args are " + args);
			RemotePlayer p = PlayerManager.instance.getPlayerByUTag(args.player_id) as RemotePlayer;
			if(p != null)
			{
				if(args.player_id != PlayerManager.instance.player1.getUTag())
				{
					Debug.Log("got from" + args.player_id);
					Debug.Log("im" + PlayerManager.instance.player1.getUTag());
				}
				RemotePlayerController rpc = p.gameObject.GetComponent<RemotePlayerController>();
				if(rpc != null)
				{
					rpc.setPositionData(
						new Vector3(args.ppx, args.ppy, args.ppz),
						new Vector3(args.pvx, args.pvy, args.pvz),
						new Quaternion(args.prx, args.pry, args.prz, args.prw),
						new Vector3(args.wpx, args.wpy, args.wpz),
						new Vector3(args.wvx, args.wvy, args.wvz),
						new Quaternion(args.wrx, args.wry, args.wrz, args.wrw));
				}
				else
				{
					Debug.Log("rpc is null");
				}


			}
		}
		else
		{
			Debug.Log("me args are " + args);
		}
	}

	public void OnResponseJoin(ExtendedEventArgs eventArgs)
	{
		ResponseJoinEventArgs args = eventArgs as ResponseJoinEventArgs;

		if (args.status == 0)
		{
			/*if(PlayerManager.instance.player1.getUTag() == -1)
			{
				PlayerManager.instance.player1.setUTag(args.user_id);
			}
			else if(PlayerManager.instance.player1.getUTag() != -1)
			{
				Player newPlayer = PlayerManager.instance.createNewPlayer<RemotePlayer>(PlayerManager.instance.guy);
				newPlayer.setUTag(args.user_id);


			}*/
			if(PlayerManager.instance.player1.getUTag() == -1)
			{
				PlayerManager.instance.player1.setUTag(args.user_id);
				Debug.Log("i am now " + args.user_id);
				if(args.op_id > 0)
				{
					Player newPlayer = PlayerManager.instance.createNewPlayer<RemotePlayer>(PlayerManager.instance.remoteguy);
					newPlayer.setUTag(args.op_id);
				}
				else
				{
					Debug.Log("op_id" + args.op_id);
				}
			}
			else
			{
				if(args.user_id > 0)
				{
					Debug.Log("theres a new player " + args.user_id);
					Player newPlayer = PlayerManager.instance.createNewPlayer<RemotePlayer>(PlayerManager.instance.remoteguy);
					newPlayer.setUTag(args.user_id);
				}
			}
			if(args.user_id > 2)
			{
				Debug.Log("ERROR: Invalid user_id in ResponseJoin: " + args.user_id);
				//messageBoxMsg.text = "Error joining game. Network returned invalid response.";
				//messageBox.SetActive(true);
				return;
			}
			Constants.USER_ID = args.user_id;
			Constants.OP_ID = 3 - args.user_id;

			if (args.op_id > 0)
			{
				if (args.op_id == Constants.OP_ID)
				{
					//opponentName.text = args.op_name;
					//opReady = args.op_ready;
				}
				else
				{
					Debug.Log("ERROR: Invalid op_id in ResponseJoin: " + args.op_id);
					//messageBoxMsg.text = "Error joining game. Network returned invalid response.";
					//messageBox.SetActive(true);
					return;
				}
			}
			else
			{
				//opponentName.text = "Waiting for opponent";
			}

			//playerInput.SetActive(true);
			//opponentName.gameObject.SetActive(true);
			//playerName.gameObject.SetActive(false);
			//opponentInput.SetActive(false);

			//rootMenuPanel.SetActive(false);
			//networkMenuPanel.SetActive(true);
		}
		else
		{
			Debug.Log("server is full");
			//messageBoxMsg.text = "Server is full.";
			//messageBox.SetActive(true);
		}
	}
}
