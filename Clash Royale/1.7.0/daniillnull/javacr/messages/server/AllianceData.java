package daniillnull.javacr.messages.server;

import daniillnull.javacr.game.Alliance;
import daniillnull.javacr.game.Player;
import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.util.Iterator;

public class AllianceData extends Packet {
   Alliance all;

   public AllianceData(Alliance all) {
      this.id = 24301;
      this.all = all;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      this.all.encode(d);
      d.writeInt(this.all.descr.length());
      d.write(this.all.descr.getBytes());
      d.write(this.all.players.size());
      Iterator var4 = this.all.players.iterator();

      while(var4.hasNext()) {
         Player player = (Player)var4.next();
         player.encodeAsMember(d);
      }

      d.write(1);
      d.write(0);
      d.write(encodeVInt(259200));
      d.write(encodeVInt(3250));
      d.writeInt(0);
      this.data = b.toByteArray();
   }
}
