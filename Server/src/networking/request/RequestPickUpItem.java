package networking.request;

import core.NetworkManager;
import model.Player;
import networking.response.ResponsePickUpItem;
import utility.DataReader;

import java.io.IOException;




public class RequestPickUpItem  extends GameRequest{
    //private int pieceIndex, x, y;
    private int uTag;
    // Responses
    private ResponsePickUpItem responsePickUpItem;

    public RequestPickUpItem() {
        responses.add(responsePickUpItem = new ResponsePickUpItem());
    }

    @Override
    public void parse() throws IOException {
        uTag = DataReader.readInt(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responsePickUpItem.setPlayer(player);
        responsePickUpItem.setData(uTag);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responsePickUpItem);
    }
}