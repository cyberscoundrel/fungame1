package networking.request;

import core.NetworkManager;
import model.Player;
import networking.response.ResponseChangePosition;
import utility.DataReader;
import utility.Log;

import java.io.IOException;




public class RequestChangePosition  extends GameRequest{
    int player_id;
    //private int pieceIndex, x, y;
    private float[] locationData;
    // Responses
    private ResponseChangePosition responseChangePosition;

    public RequestChangePosition() {
        responses.add(responseChangePosition = new ResponseChangePosition());
        locationData = new float[20];
    }

    @Override
    public void parse() throws IOException {
        player_id = DataReader.readInt(dataInput);
        for(int index0 = 0; index0 < 20; ++index0)
        {
            locationData[index0] = DataReader.readFloat(dataInput);
            Log.printf("parsed in float %f\n", locationData[index0]);
        }
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseChangePosition.setPlayer(player);
        responseChangePosition.setData(player.getID(), locationData);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseChangePosition);
    }
}
