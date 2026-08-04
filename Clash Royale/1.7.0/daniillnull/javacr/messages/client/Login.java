package daniillnull.javacr.messages.client;

import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.IOException;

public class Login extends Packet {
   public int vid;

   public void process() throws IOException {
      DataInputStream s = new DataInputStream(new ByteArrayInputStream(this.data));
      this.vid = (int)s.readLong();
   }
}
