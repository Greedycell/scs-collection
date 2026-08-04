package daniillnull.javacr.game;

import daniillnull.javacr.messages.Packet;
import daniillnull.javacr.messages.server.AllianceMessage;
import java.io.DataInputStream;
import java.io.DataOutput;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Random;
import java.util.concurrent.CopyOnWriteArrayList;

public class Alliance {
   private static HashMap<Integer, Alliance> alliances = new HashMap();
   public int id;
   public int badge;
   public int reqsc;
   public int orig;
   public int score;
   public String name;
   public String descr;
   public CopyOnWriteArrayList<Player> players;
   public transient CopyOnWriteArrayList<AllianceChatMessage> messages;

   static {
      load(0).descr = "Official clan";
   }

   private Alliance(int id) {
      this.id = id;
      this.players = new CopyOnWriteArrayList();
      this.messages = new CopyOnWriteArrayList();
      this.name = "DCS Team";
      this.descr = "";
   }

   public static Alliance load(int id) {
      if (id == 0) {
         for(id = 1; alliances.containsKey(id); ++id) {
         }

         alliances.put(id, new Alliance(id));
      }

      return (Alliance)alliances.get(id);
   }

   public static void loadSavedData() throws IOException {
      if ((new File("alliances.dat")).exists()) {
         DataInputStream d = new DataInputStream(new FileInputStream("alliances.dat"));
         int count = d.readInt();

         for(int i = 0; i < count; ++i) {
            Alliance all = new Alliance(d.readInt());
            all.badge = d.readShort();
            all.orig = d.readShort();
            all.reqsc = d.readShort();
            all.name = Packet.readString(d);
            all.descr = Packet.readString(d);
            int pls = d.readInt();

            for(int q = 0; q < pls; ++q) {
               Player pl = Player.load(d.readInt());
               pl.all = all;
               all.players.add(pl);
            }

            d.skip(10L);
            alliances.put(all.id, all);
         }

         d.close();
      }
   }

   public static void saveData() throws IOException {
      DataOutputStream d = new DataOutputStream(new FileOutputStream("alliances.dat"));
      d.writeInt(alliances.size());
      Iterator var3 = alliances.values().iterator();

      while(var3.hasNext()) {
         Alliance all = (Alliance)var3.next();
         d.writeInt(all.id);
         d.writeShort(all.badge);
         d.writeShort(all.orig);
         d.writeShort(all.reqsc);
         d.writeInt(all.name.length());
         d.write(all.name.getBytes());
         d.writeInt(all.descr.length());
         d.write(all.descr.getBytes());
         d.writeInt(all.players.size());
         int tmpscore = 0;

         Player pl;
         for(Iterator var5 = all.players.iterator(); var5.hasNext(); tmpscore += pl.score) {
            pl = (Player)var5.next();
            d.writeInt(pl.id);
         }

         all.score = tmpscore;
         d.write(new byte[10]);
      }

      d.close();
   }

   public static List<Alliance> joinable() {
      int alls = alliances.size();
      int rets = alls > 20 ? 20 : alls;
      int offset = alls > rets ? (new Random()).nextInt(alls - rets) : 0;
      return (new ArrayList(alliances.values())).subList(offset, offset + rets);
   }

   public void encode(DataOutput d) throws IOException {
      d.writeLong((long)this.id);
      d.writeInt(this.name.length());
      d.write(this.name.getBytes());
      d.write(16);
      d.write(Packet.encodeVInt(this.badge));
      d.write(1);
      d.write(this.players.size());
      d.write(Packet.encodeVInt(this.score));
      d.write(Packet.encodeVInt(this.reqsc));
      d.writeLong(57L);
      d.write(Packet.encodeVInt(this.orig));
   }

   public void addMessage(AllianceChatMessage message) {
      this.messages.add(message);
      if (this.messages.size() > 50) {
         this.messages.remove(0);
      }

      Iterator var3 = this.players.iterator();

      while(var3.hasNext()) {
         Player pl = (Player)var3.next();
         if (pl.current != null) {
            try {
               pl.current.os.write(new AllianceMessage(message));
            } catch (Exception var5) {
            }
         }
      }

   }
}
