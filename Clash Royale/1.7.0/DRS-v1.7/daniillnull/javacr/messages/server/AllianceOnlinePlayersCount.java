package daniillnull.javacr.messages.server;

import daniillnull.javacr.game.Alliance;
import daniillnull.javacr.game.Player;
import daniillnull.javacr.messages.Packet;
import java.io.IOException;
import java.util.Iterator;

public class AllianceOnlinePlayersCount extends Packet {
   Alliance all;

   public AllianceOnlinePlayersCount(Alliance all) {
      this.id = 20207;
      this.all = all;
   }

   public void process() throws IOException {
      this.data = new byte[1];
      Iterator var2 = this.all.players.iterator();

      while(var2.hasNext()) {
         Player pl = (Player)var2.next();
         if (pl.current != null) {
            ++this.data[0];
         }
      }

   }
}
