package networking.response;

import metadata.Constants;
import model.Player;
import utility.GamePacket;
//import utility.Log;

public class ResponsePickUpItem extends GameResponse{
    private Player player;
    int uTag;

    public ResponsePickUpItem() {
        responseCode = Constants.SMSG_PICKUP_ITEM;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        for(int index0 = 0; index0 < 20; ++index0)
        {
            packet.addInt32(uTag);
        }


        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setData(int utag) {
        this.uTag = uTag;
    }
}
