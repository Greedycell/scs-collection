package daniillnull.javacr.messages.client;

import daniillnull.javacr.messages.Packet;
import java.io.IOException;
import java.util.Arrays;

public class SendAllianceMessage extends Packet {
   public byte[] message;

   public void process() throws IOException {
      this.message = Arrays.copyOfRange(this.data, 4, (this.data[3] & 255) + 4);
   }
}
