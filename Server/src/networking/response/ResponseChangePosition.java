package networking.response;

import metadata.Constants;
import model.Player;
import utility.GamePacket;
//import utility.Log;

public class ResponseChangePosition extends GameResponse{
    private Player player;
    int player_id;
    float[] locationData;

    public ResponseChangePosition() {
        responseCode = Constants.SMSG_CHANGEPOSITION;
        locationData = new float[20];
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player_id);
        for(int index0 = 0; index0 < 20; ++index0)
        {
            packet.addFloat(locationData[index0]);
        }


        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setData(int player_id, float[] locationData)
    {
        this.player_id = player_id;
        this.locationData = locationData;
    }
}
