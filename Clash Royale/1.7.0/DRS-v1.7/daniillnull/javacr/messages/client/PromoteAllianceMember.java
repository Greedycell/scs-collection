package daniillnull.javacr.messages.client;

import daniillnull.javacr.messages.Packet;
import java.io.IOException;
import java.math.BigInteger;
import java.util.Arrays;

public class PromoteAllianceMember extends Packet {
   public int uid;
   public byte role;

   public void process() throws IOException {
      this.uid = (new BigInteger(Arrays.copyOf(this.data, 8))).intValue();
      this.role = this.data[8];
   }
}
