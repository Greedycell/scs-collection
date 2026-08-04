package daniillnull.javacr.game;

import daniillnull.javacr.messages.Packet;
import java.io.DataOutput;
import java.io.IOException;

public class AllianceChatMessage {
   public byte[] message;
   public byte[] pname;
   public byte[] mname;
   public int type;
   public int pid;
   public int prole;
   public long time = System.currentTimeMillis();

   public static AllianceChatMessage createText(byte[] message, Player sender) {
      AllianceChatMessage m = new AllianceChatMessage();
      m.message = message;
      m.pname = sender.name.getBytes();
      m.pid = sender.id;
      m.prole = sender.role;
      return m;
   }

   public static AllianceChatMessage createEvent(int type, Player sender, String mname) {
      AllianceChatMessage m = new AllianceChatMessage();
      m.mname = mname.getBytes();
      m.pid = sender.id;
      m.pname = sender.name.getBytes();
      m.type = type;
      return m;
   }

   public void encode(DataOutput d) throws IOException {
      d.write(this.type == 0 ? 2 : 4);
      d.write(0);
      d.write(Packet.encodeVInt(this.hashCode() & 1048575));
      d.write(0);
      d.write(Packet.encodeVInt(this.pid));
      d.write(0);
      d.write(Packet.encodeVInt(this.pid));
      d.writeInt(this.pname.length);
      d.write(this.pname);
      d.write(new byte[]{10, (byte)this.prole});
      d.write(Packet.encodeVInt((int)(System.currentTimeMillis() / 1000L - this.time / 1000L)));
      d.write(0);
      if (this.type == 0) {
         d.writeInt(this.message.length);
         d.write(this.message);
      } else {
         d.write(this.type);
         d.write(0);
         d.write(0);
         d.writeInt(this.mname.length);
         d.write(this.mname);
      }

   }
}
