package daniillnull.javacr.messages.server;

import daniillnull.javacr.game.Alliance;
import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.util.Iterator;
import java.util.List;

public class JoinableAlliances extends Packet {
   public JoinableAlliances() {
      this.id = 24304;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      List<Alliance> alls = Alliance.joinable();
      d.write(alls.size());
      Iterator var5 = alls.iterator();

      while(var5.hasNext()) {
         Alliance all = (Alliance)var5.next();
         all.encode(d);
      }

      this.data = b.toByteArray();
   }
}
