package daniillnull.javacr.messages.server;

import daniillnull.javacr.game.AllianceChatMessage;
import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.util.Iterator;
import java.util.List;

public class AllianceMessageStream extends Packet {
   public List<AllianceChatMessage> messages;

   public AllianceMessageStream(List<AllianceChatMessage> messages) {
      this.id = 24311;
      this.messages = messages;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      d.write(this.messages.size());
      Iterator var4 = this.messages.iterator();

      while(var4.hasNext()) {
         AllianceChatMessage message = (AllianceChatMessage)var4.next();
         message.encode(d);
      }

      this.data = b.toByteArray();
   }
}
