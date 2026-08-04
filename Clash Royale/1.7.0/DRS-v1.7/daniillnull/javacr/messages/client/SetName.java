package daniillnull.javacr.messages.client;

import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayInputStream;
import java.io.IOException;

public class SetName extends Packet {
   public String name;

   public void process() throws IOException {
      ByteArrayInputStream s = new ByteArrayInputStream(this.data);
      s.read(new byte[3]);
      byte[] tmp = new byte[s.read()];
      s.read(tmp);
      this.name = new String((new String(tmp)).getBytes("ASCII"));
   }
}
