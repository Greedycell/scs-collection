package daniillnull.javacr.messages.server;

import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class LoginOk extends Packet {
   int vid;
   String timestamp1 = Long.toString(System.currentTimeMillis());

   public LoginOk(int vid) {
      this.id = 20104;
      this.vid = vid;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      d.writeLong((long)this.vid);
      d.writeLong((long)this.vid);
      d.write(new byte[]{0, 0, 0, 4, 112, 97, 115, 115});
      d.writeInt(-1);
      d.writeInt(-1);
      d.writeInt(0);
      d.write(0);
      d.write(0);
      d.write(new byte[]{0, 0, 0, 4, 112, 114, 111, 100});
      d.writeInt(0);
      d.write(0);
      d.writeInt(this.timestamp1.length());
      d.write(this.timestamp1.getBytes());
      d.writeInt(-1);
      d.writeInt(-1);
      d.write(0);
      d.writeInt(-1);
      d.writeInt(0);
      d.writeInt(0);
      this.data = b.toByteArray();
   }
}
