package daniillnull.javacr.game;

import daniillnull.javacr.messages.Packet;
import daniillnull.javacr.server.Session;
import daniillnull.util.Helpers;
import java.io.DataInputStream;
import java.io.DataOutput;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.HashMap;
import java.util.Iterator;

public class Player {
   private static HashMap<Integer, Player> players = new HashMap();
   public int id;
   public String name = "";
   public byte lvl = 8;
   public byte xp = 0;
   public byte arena = 1;
   public byte role = 1;
   public int score = 0;
   public int gems = 1000000000;
   public int gold = 1000000000;
   public int kickedfrom;
   public Alliance all;
   public Session current;
   public int[][] deck = new int[][]{{26, 0, 12}, {26, 1, 12}, {26, 2, 12}, {26, 3, 10}, {26, 4, 7}, {26, 5, 12}, {26, 6, 7}, {26, 7, 7}};
   public int[][] unlockedCards = new int[][]{{26, 8, 12}, {26, 9, 7}, {26, 10, 12}, {26, 11, 10}, {26, 12, 7}, {26, 13, 12}, {26, 14, 10}, {26, 15, 7}, {26, 16, 7}, {26, 17, 10}, {26, 18, 10}, {26, 19, 12}, {26, 20, 7}, {26, 21, 10}, {26, 22, 12}, {26, 23, 4}, {26, 24, 12}, {26, 25, 7}, {26, 26, 4}, {26, 27, 7}, {26, 28, 10}, {26, 29, 4}, {26, 30, 12}, {26, 31, 12}, {26, 32, 4}, {26, 33, 4}, {26, 34, 7}, {26, 35, 4}, {26, 36, 10}, {26, 37, 4}, {26, 38, 10}, {26, 39, 10}, {26, 40, 10}, {26, 41, 12}, {26, 42, 4}, {26, 43, 12}, {26, 45, 7}, {27, 0, 12}, {27, 1, 10}, {27, 2, 12}, {27, 3, 10}, {27, 4, 10}, {27, 5, 10}, {27, 6, 12}, {27, 7, 10}, {27, 8, 7}, {27, 9, 10}, {27, 10, 10}, {28, 0, 10}, {28, 1, 12}, {28, 2, 7, 10}, {28, 3, 10}, {28, 4, 7}, {28, 5, 7}, {28, 6, 7}, {28, 7, 7}, {28, 8, 12}, {28, 9, 7}, {28, 10, 4}, {28, 11, 4}, {28, 12, 7}, {28, 13, 7}};

   private Player(int id) {
      this.id = id;
   }

   public static Player load(int id) {
      if (id == 0) {
         for(id = 1; players.containsKey(id); ++id) {
         }
      }

      if (!players.containsKey(id)) {
         players.put(id, new Player(id));
      }

      return (Player)players.get(id);
   }

   public static void loadSavedData() throws IOException {
      if ((new File("players.dat")).exists()) {
         DataInputStream d = new DataInputStream(new FileInputStream("players.dat"));
         int count = d.readInt();

         for(int i = 0; i < count; ++i) {
            Player pl = new Player(d.readInt());
            pl.name = Packet.readString(d);
            pl.arena = (byte)d.read();
            pl.lvl = (byte)d.read();
            pl.role = (byte)d.read();
            pl.score = d.readInt();
            pl.gems = d.readInt();
            pl.gold = d.readInt();

            int q;
            for(q = 0; q < 8; ++q) {
               pl.deck[q][0] = d.read();
               pl.deck[q][1] = d.read();
               pl.deck[q][2] = d.read();
            }

            pl.unlockedCards = new int[d.readInt()][];

            for(q = 0; q < pl.unlockedCards.length; ++q) {
               pl.unlockedCards[q] = new int[]{d.read(), d.read(), d.read()};
            }

            pl.kickedfrom = d.readInt();
            d.skip(16L);
            players.put(pl.id, pl);
         }

         d.close();
      }
   }

   public static void saveData() throws IOException {
      DataOutputStream d = new DataOutputStream(new FileOutputStream("players.dat"));
      d.writeInt(players.size());
      Iterator var2 = players.values().iterator();

      while(var2.hasNext()) {
         Player pl = (Player)var2.next();
         d.writeInt(pl.id);
         d.writeInt(pl.name.length());
         d.write(pl.name.getBytes());
         d.write(pl.arena);
         d.write(pl.lvl);
         d.write(pl.role);
         d.writeInt(pl.score);
         d.writeInt(pl.gems);
         d.writeInt(pl.gold);

         int q;
         for(q = 0; q < 8; ++q) {
            d.write(pl.deck[q][0]);
            d.write(pl.deck[q][1]);
            d.write(pl.deck[q][2]);
         }

         d.writeInt(pl.unlockedCards.length);

         for(q = 0; q < pl.unlockedCards.length; ++q) {
            d.write(pl.unlockedCards[q][0]);
            d.write(pl.unlockedCards[q][1]);
            d.write(pl.unlockedCards[q][2]);
         }

         d.writeInt(pl.kickedfrom);
         d.write(new byte[16]);
      }

      d.close();
   }

   public void encode(DataOutput d) throws IOException {
      d.writeLong((long)this.id);
      d.write(Helpers.parseHexBinary("0500A8E4D201A8F3D20181A5D1870B00030880EAE51881EAE5188DEAE51881FCD91A80FCD91A83EAE51800000880EAE51881EAE5188DEAE51881FCD91A80FCD91A83EAE51800000880EAE51881EAE5188DEAE51881FCD91A80FCD91A83EAE5180000FF"));

      int i;
      for(i = 0; i < 8; ++i) {
         d.write(this.deck[i][0]);
         d.write(this.deck[i][1]);
         d.write(this.deck[i][2]);
         d.write(0);
         d.write(0);
         d.writeShort(0);
         d.write(0);
      }

      d.write(Packet.encodeVInt(this.unlockedCards.length));

      for(i = 0; i < this.unlockedCards.length; ++i) {
         d.write(this.unlockedCards[i][0]);
         d.write(this.unlockedCards[i][1]);
         d.write(this.unlockedCards[i][2]);
         d.write(0);
         d.write(0);
         d.writeShort(0);
         d.write(0);
      }

      d.write(Helpers.parseHexBinary("0000007F7F0000000000007F0113070102007F000000000000000000000000000000000000007F010000000000000002"));
      d.write(new byte[]{this.lvl, 54, this.arena, 0, 0, 0, 0, 0, 0, 0});
      d.write(Helpers.parseHexBinary("0000007f00007f00007f130e9705000002b3200500011a27010900000000F807"));

      for(i = 0; i < 8; ++i) {
         d.write(this.deck[i][0]);
         d.write(this.deck[i][1]);
         d.write(this.deck[i][2]);
         d.write(0);
         d.write(0);
         d.writeShort(0);
         d.write(0);
      }

      d.write(Helpers.parseHexBinary("09A5EAE518A6EAE518A7EAE518A8EAE518AAEAE518ABEAE5188AFCD91A8CFCD91A8DFCD91A0000000000010000AEDFC6870B01000000000000007F000000"));
      d.write(Helpers.parseHexBinary("0113940301ba09007f94198e0404029c9f038019587c53c158804841adaf3c"));
      this.encodeEntry(d);
      d.write(new byte[]{0, 0, 0, 0, 0, 6, 0, 0, 0});
      d.write(Helpers.parseHexBinary("FBFADD8003BFDFC6870B"));
   }

   public void encodeEntry(DataOutput d) throws IOException {
      d.write(Packet.encodeVInt(0));
      d.write(Packet.encodeVInt(this.id));
      d.write(Packet.encodeVInt(0));
      d.write(Packet.encodeVInt(this.id));
      d.write(Packet.encodeVInt(0));
      d.write(Packet.encodeVInt(this.id));
      d.writeInt(this.name.length());
      d.write(this.name.getBytes());
      d.write(new byte[]{(byte)(this.name.isEmpty() ? 127 : 0), 54, this.arena});
      d.write(Packet.encodeVInt(this.score));
      d.write(new byte[6]);
      d.write(Packet.encodeVInt(this.score));
      d.write(Packet.encodeVInt(this.score));
      d.write(7);
      d.write(1);
      d.writeShort(1281);
      d.write(Packet.encodeVInt(0)); // Resources
      d.write(Packet.encodeVInt(2)); // Resources
      {
         d.write(Packet.encodeVInt(5)); // Type
         d.write(Packet.encodeVInt(1)); // ID
         d.write(Packet.encodeVInt(this.gold)); // Gold Amount
         d.write(Packet.encodeVInt(5)); // Type
         d.write(Packet.encodeVInt(5)); // ID
         d.write(Packet.encodeVInt(this.gold)); // Gold Amount
      }
      d.write(Packet.encodeVInt(0));
      d.write(Packet.encodeVInt(0));
      d.write(0);
      d.write(0);
      d.write(0);
      d.write(Packet.encodeVInt(this.gems));
      d.write(Packet.encodeVInt(this.gems));
      d.write(Packet.encodeVInt(this.xp));
      d.write(Packet.encodeVInt(this.lvl));
      if (this.all != null) {
         d.write(1); // NameSet
         d.write(this.name.isEmpty() ? 8 : 9);
         d.write(Packet.encodeVInt(0));
         d.write(Packet.encodeVInt(this.all.id));
         d.writeInt(this.all.name.length());
         d.write(this.all.name.getBytes());
         d.write(16);
         d.write(Packet.encodeVInt(this.all.badge));
         d.write(this.role);
      } else {
         d.write(1); // NameSet
         d.write(this.name.isEmpty() ? 0 : 7);
      }

   }

   public void encodeAsMember(DataOutput d) throws IOException {
      d.writeLong((long)this.id);
      d.writeInt(-1);
      d.writeInt(this.name.length());
      d.write(this.name.getBytes());
      d.write(new byte[]{54, this.arena, this.role, this.lvl});
      d.write(Packet.encodeVInt(this.score));
      d.write(0);
      d.write(new byte[]{20, 48, 49, 0, 0, 127, -83, -81, 60, 6});
      d.writeLong((long)this.id);
   }

   public void upgradeCard(short id) {
      int[][] var5;
      int var4 = (var5 = this.unlockedCards).length;

      int[] card;
      int var10002;
      int var3;
      for(var3 = 0; var3 < var4; ++var3) {
         card = var5[var3];
         if (card[0] == id >> 8 && card[1] == (id & 255)) {
            var10002 = card[2]++;
            break;
         }
      }

      var4 = (var5 = this.deck).length;

      for(var3 = 0; var3 < var4; ++var3) {
         card = var5[var3];
         if (card[0] == id >> 8 && card[1] == (id & 255)) {
            var10002 = card[2]++;
            break;
         }
      }

   }
}
