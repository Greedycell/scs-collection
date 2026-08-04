package daniillnull.javacr.messages.client;

import daniillnull.javacr.game.AllianceChatMessage;
import daniillnull.javacr.game.Player;
import daniillnull.javacr.messages.Packet;
import daniillnull.javacr.messages.server.AllianceCommandOk;
import daniillnull.javacr.messages.server.ChestData;
import daniillnull.javacr.messages.server.SectorState;
import daniillnull.javacr.server.Session;
import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.IOException;
import java.util.Arrays;

public class ExecuteCmd extends Packet {
   public void process() throws IOException {
   }

   public void executeAll(Session s) throws IOException {
      DataInputStream d = new DataInputStream(new ByteArrayInputStream(this.data));
      readVInt(d);
      readVInt(d);
      int count = d.read();

      for(int i = 0; i < count; ++i) {
         this.execute(d, s);
      }

   }

   protected void execute(DataInputStream d, Session s) throws IOException {
      int type = readVInt(d);
      if (type < 500) {
         this.skipS(d, type);
      } else {
         readVInt(d);
         readVInt(d);
         readVInt(d);
         readVInt(d);
         switch(type) {
         case 1:
            System.out.println("Placing cards isn't serversided yet!");
            return;
         case 500:
            int f = d.read();
            int t = d.read();
            int n = d.read();
            int[] w;
            if (n == 127) {
               w = s.player.deck[t];
               s.player.deck[t] = s.player.unlockedCards[f];
               s.player.unlockedCards[f] = w;
            } else if (f == 127) {
               w = s.player.deck[t];
               s.player.deck[t] = s.player.deck[n];
               s.player.deck[n] = w;
            }

            return;
         case 501:
            d.read();
            throw new RuntimeException();
         case 504:
            short id = d.readShort();
            s.player.upgradeCard(id);
            return;
         case 509:
            s.os.write(new ChestData(4));
            return;
         case 514:
            readString(d);
            Player tt = Player.load((int)d.readLong());
            if (s.player.all == tt.all) {
               try {
                  tt.current.os.write(new AllianceCommandOk(tt.all, 141, true));
               } catch (Exception var11) {
                  tt.kickedfrom = s.player.all.id;
               }

               s.player.all.players.remove(tt);
               s.player.all.addMessage(AllianceChatMessage.createEvent(1, tt, s.player.name));
               tt.all = null;
            }

            return;
         case 516:
            d.read();
            int chestid = d.read();
            if (chestid == 60) {
               s.os.write(new ChestData(2));
            } else if (chestid == 61) {
               s.os.write(new ChestData(3));
            } else {
               s.os.write(new ChestData(1));
            }

            return;
         case 523:
         case 526:
         case 545:
            return;
         case 525:
            s.os.write(new SectorState(s.player, 2));
            d.skip(2L);
            return;
         case 539:
            d.skip(1L);
            return;
         case 550:
            s.os.write(new ChestData(5));
            return;
         case 551:
            d.read();
            d.read();
            readVInt(d);
            readVInt(d);
            d.readInt();
            readVInt(d);
            readVInt(d);
            return;
         default:
            System.out.println("Unhandled: type: " + type + ", data: " + Arrays.toString(this.data) + ", av: " + d.available());
         }
      }
   }

   protected void skipS(DataInputStream d, int type) throws IOException {
      switch(type) {
      case 201:
         readString(d);
         d.skip(6L);
         break;
      case 202:
      case 203:
      case 204:
      case 207:
      case 208:
      case 209:
      default:
         System.out.println("Unhandled: type: " + type + ", data: " + Arrays.toString(this.data) + ", av: " + d.available());
         break;
      case 205:
         d.skip(10L);
         break;
      case 206:
         d.skip(8L);
         readString(d);
         d.skip(1L);
         readVInt(d);
         d.skip(2L);
         break;
      case 210:
         d.skip(1L);
         if (d.read() != 0) {
            throw new RuntimeException("This command cant contain cards if it received form client");
         }

         d.skip(5L);
      }

      readVInt(d);
      readVInt(d);
      readVInt(d);
      readVInt(d);
   }
}
