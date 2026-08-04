package daniillnull.javacr.messages.server;

import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class ChangeNameOk extends Packet {
   String newName;

   public ChangeNameOk(String newName) {
      this.id = 24111;
      this.newName = newName;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      d.write(137);
      d.write(3);
      d.writeInt(this.newName.length());
      d.write(this.newName.getBytes());
      d.writeInt(0);
      d.write(1);
      d.write(2);
      this.data = b.toByteArray();
   }
}
