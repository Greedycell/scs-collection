package daniillnull.javacr.messages.client;

import daniillnull.javacr.messages.Packet;
import java.io.IOException;
import java.util.Arrays;

public class SessionReq extends Packet {
   public String rhash;

   public void process() throws IOException {
      if (this.data[11] != 2) {
         throw new IOException();
      } else {
         this.rhash = new String(Arrays.copyOfRange(this.data, 24, 64));
      }
   }
}
