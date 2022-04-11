using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseChangePositionArgs : ExtendedEventArgs
{
	public int player_id{ get; set; }
	public float ppx { get; set; } // 0 = success
	public float ppy { get; set; }
	public float ppz { get; set; }
	public float pvx { get; set; }
	public float pvy { get; set; }
	public float pvz { get; set; }
	public float prx { get; set; }
	public float pry { get; set; }
	public float prz { get; set; }
	public float prw { get; set; }
	public float wpx { get; set; }
	public float wpy { get; set; }
	public float wpz { get; set; }
	public float wvx { get; set; }
	public float wvy { get; set; }
	public float wvz { get; set; }
	public float wrx { get; set; }
	public float wry { get; set; }
	public float wrz { get; set; }
	public float wrw { get; set; }


	public ResponseChangePositionArgs()
	{
		event_id = Constants.SMSG_CHANGEPOSITION;
	}
}

public class ResponseChangePosition : NetworkResponse
{
	int playerId;
	Vector3 playerPosition, playerVelocity, weaponPosition, weaponVelocity;
	Quaternion playerRotation, weaponRotation;

	public ResponseChangePosition()
	{
	}

	public override void parse()
	{
		playerId = DataReader.ReadInt(dataStream);
		playerPosition = new Vector3(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream));
		playerVelocity = new Vector3(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream));
		weaponPosition = new Vector3(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream));
		weaponVelocity = new Vector3(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream));
		playerRotation = new Quaternion(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream));
		playerRotation = new Quaternion(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream));


		
	}

	public override ExtendedEventArgs process()
	{
		ResponseChangePositionArgs args = new ResponseChangePositionArgs();
		args.player_id = playerId;
		args.ppx = playerPosition.x;
		args.ppy = playerPosition.y;
		args.ppz = playerPosition.z;
		args.pvx = playerVelocity.x;
		args.pvy = playerVelocity.y;
		args.pvz = playerVelocity.z;
		args.prx = playerRotation.x;
		args.pry = playerRotation.y;
		args.prz = playerRotation.z;
		args.prw = playerRotation.w;
		args.wpx = weaponPosition.x;
		args.wpy = weaponPosition.y;
		args.wpz = weaponPosition.z;
		args.wvz = weaponVelocity.x;
		args.wvy = weaponVelocity.y;
		args.wvz = weaponVelocity.z;
		args.wrx = weaponRotation.x;
		args.wry = weaponRotation.y;
		args.wrz = weaponRotation.z;
		args.wrw = weaponRotation.w;

		return args;
	}
}