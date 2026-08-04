package daniillnull.javacr.messages.client;

import daniillnull.javacr.messages.Packet;
import java.io.IOException;
import java.math.BigInteger;

public class GetProfileData extends Packet {
   public int uid;

   public void process() throws IOException {
      this.uid = (new BigInteger(this.data)).intValue();
   }
}
