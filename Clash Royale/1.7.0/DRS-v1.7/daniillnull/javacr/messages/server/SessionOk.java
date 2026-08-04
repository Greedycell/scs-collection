package daniillnull.javacr.messages.server;

import daniillnull.javacr.messages.Packet;
import java.io.IOException;

public class SessionOk extends Packet {
   public SessionOk() {
      this.id = 20100;
   }

   public void process() throws IOException {
      this.data = new byte[]{0, 0, 0, 24, 99, 104, 101, 99, 107, 32, 111, 107, 32, 99, 104, 101, 99, 107, 32, 111, 107, 32, 107, 101, 101, 101, 101, 107};
   }
}
