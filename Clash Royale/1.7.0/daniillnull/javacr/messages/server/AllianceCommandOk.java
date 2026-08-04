package daniillnull.javacr.messages.server;

import daniillnull.javacr.game.Alliance;
import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class AllianceCommandOk extends Packet {
   Alliance all;
   int type;
   boolean adv;

   public AllianceCommandOk(Alliance all, int type, boolean adv) {
      this.id = 24111;
      this.all = all;
      this.type = type;
      this.adv = adv;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      d.write(this.type);
      d.write(3);
      d.writeLong((long)this.all.id);
      if (this.type != 141) {
         d.writeInt(this.all.name.length());
         d.write(this.all.name.getBytes());
         d.write(16);
         d.write(encodeVInt(this.all.badge));
      }

      d.writeBoolean(this.adv);
      this.data = b.toByteArray();
   }
}
