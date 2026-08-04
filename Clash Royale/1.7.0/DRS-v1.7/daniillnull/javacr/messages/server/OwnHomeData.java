package daniillnull.javacr.messages.server;

import daniillnull.javacr.game.Player;
import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class OwnHomeData extends Packet {
   Player own;

   public OwnHomeData(Player own) {
      this.id = 24101;
      this.own = own;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      this.own.encode(d);
      this.data = b.toByteArray();
   }
}
