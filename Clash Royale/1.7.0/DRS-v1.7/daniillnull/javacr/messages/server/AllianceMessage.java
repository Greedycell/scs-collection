package daniillnull.javacr.messages.server;

import daniillnull.javacr.game.AllianceChatMessage;
import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class AllianceMessage extends Packet {
   AllianceChatMessage message;

   public AllianceMessage(AllianceChatMessage message) {
      this.id = 24312;
      this.message = message;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      this.message.encode(d);
      this.data = b.toByteArray();
   }
}
