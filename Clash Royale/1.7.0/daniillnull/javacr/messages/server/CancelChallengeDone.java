package daniillnull.javacr.messages.server;

import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class CancelChallengeDone extends Packet {
   public CancelChallengeDone() {
      this.id = 24124;
   }
   
   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      d.write(0);
      this.data = b.toByteArray();
   }
}
