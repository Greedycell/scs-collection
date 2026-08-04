package daniillnull.javacr.messages.client;

import daniillnull.javacr.messages.Packet;
import java.io.IOException;
import java.math.BigInteger;
import java.util.Arrays;

public class GetAllianceData extends Packet {
   public int cid;

   public void process() throws IOException {
      this.cid = (new BigInteger(Arrays.copyOf(this.data, 8))).intValue();
   }
}
