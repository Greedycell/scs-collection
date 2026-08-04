package daniillnull.javacr.messages.client;

import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.IOException;

public class ChangeAlliance extends Packet {
   public String descr;
   public int badge;
   public int reqsc;
   public int orig;

   public void process() throws IOException {
      DataInputStream s = new DataInputStream(new ByteArrayInputStream(this.data));
      this.descr = readString(s);
      s.read();
      this.badge = readVInt(s);
      s.read();
      this.reqsc = readVInt(s);
      s.read();
      this.orig = readVInt(s);
   }
}
