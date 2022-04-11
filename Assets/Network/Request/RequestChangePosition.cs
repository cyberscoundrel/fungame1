using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestChangePosition : NetworkRequest
{
    public RequestChangePosition()
	{
		request_id = Constants.CMSG_CHANGEPOSITION;
	}

	public void send(int playerId, Vector3 playerPosition, Vector3 playerVelocity, Quaternion playerRotation, Vector3 weaponPosition, Vector3 weaponVelocity, Quaternion weaponRotation)
	{
		packet = new GamePacket(request_id);
		packet.addInt32(playerId);
		packet.addFloat32(playerPosition.x);
		//Debug.Log("float for ppx " + playerPosition.x);
		packet.addFloat32(playerPosition.y);
		packet.addFloat32(playerPosition.z);
		packet.addFloat32(playerVelocity.x);
		packet.addFloat32(playerVelocity.y);
		packet.addFloat32(playerVelocity.z);
		packet.addFloat32(playerRotation.x);
		packet.addFloat32(playerRotation.y);
		packet.addFloat32(playerRotation.z);
		packet.addFloat32(playerRotation.w);
		packet.addFloat32(weaponPosition.x);
		packet.addFloat32(weaponPosition.y);
		packet.addFloat32(weaponPosition.z);
		packet.addFloat32(weaponVelocity.x);
		packet.addFloat32(weaponVelocity.y);
		packet.addFloat32(weaponVelocity.z);
		packet.addFloat32(weaponRotation.x);
		packet.addFloat32(weaponRotation.y);
		packet.addFloat32(weaponRotation.z);
		packet.addFloat32(weaponRotation.w);


	}
}
