package daniillnull.javacr.messages.server;

import daniillnull.javacr.game.Player;
import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class ProfileData extends Packet {
   Player own;

   public ProfileData(Player own) {
      this.id = 24113;
      this.own = own;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      d.write(3);
      d.write(255);

      for(int i = 0; i < 8; ++i) {
         d.write(this.own.deck[i][0]);
         d.write(this.own.deck[i][1]);
         d.write(this.own.deck[i][2]);
         d.write(0);
         d.write(1);
         d.writeShort(0);
         d.write(0);
      }

      d.writeLong((long)this.own.id);
      this.own.encodeEntry(d);
      d.write(new byte[]{0, 0, 0, 0, 0, 1, 0, 0, 0, 0});
      this.data = b.toByteArray();
   }
}
