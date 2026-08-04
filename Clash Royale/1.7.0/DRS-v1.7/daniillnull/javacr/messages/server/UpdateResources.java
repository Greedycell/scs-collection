package daniillnull.javacr.messages.server;

import daniillnull.javacr.messages.Packet;
import daniillnull.javacr.server.Main;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class UpdateResources extends Packet {
   public UpdateResources() {
      this.id = 20103;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      d.write(7);
      d.writeInt(Main.fingerPrint.length);
      d.write(Main.fingerPrint);
      d.writeInt(-1);
      d.writeInt(Main.fingerPrintUrl.length());
      d.write(Main.fingerPrintUrl.getBytes());
      this.data = b.toByteArray();
   }
}
