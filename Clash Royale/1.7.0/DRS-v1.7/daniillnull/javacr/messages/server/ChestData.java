package daniillnull.javacr.messages.server;

import daniillnull.javacr.messages.Packet;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Random;

public class ChestData extends Packet {
   public static byte[][] commons = new byte[][]{{26, 31}, {26, 0}, {26, 1}, {26, 2}, {26, 5}, {26, 8}, {26, 10}, {26, 13}, {26, 19}, {26, 22}, {26, 24}, {27, 0}, {27, 2}, {27, 6}, {28, 1}, {28, 8}};
   public static byte[][] rares = new byte[][]{{26, 3}, {26, 11}, {26, 14}, {26, 17}, {26, 18}, {26, 21}, {27, 10}, {27, 3}, {27, 4}, {27, 1}, {27, 5}, {27, 9}, {27, 7}, {28, 0}, {28, 3}};
   public static byte[][] epics = new byte[][]{{26, 25}, {26, 4}, {26, 6}, {26, 7}, {26, 9}, {26, 12}, {26, 15}, {26, 16}, {26, 20}, {26, 27}, {27, 8}, {28, 2}, {28, 4}, {28, 5}, {28, 6}, {28, 7}, {28, 9}};
   public static byte[][] legendaries = new byte[][]{{26, 33}, {26, 32}, {26, 29}, {26, 23}, {26, 26}};
   public ArrayList<byte[]> was = new ArrayList();
   public int type = 0;

   public ChestData(int type) {
      this.id = 24111;
      this.type = type;
   }

   public void process() throws IOException {
      ByteArrayOutputStream b = new ByteArrayOutputStream();
      DataOutputStream d = new DataOutputStream(b);
      Random r = new Random();
      d.write(146);
      d.write(3);
      d.write(1);
      switch(this.type) {
      case 1:
         this.genMagicalChest(d, r, 114, 22, 3, 7);
         break;
      case 2:
         this.genGiantChest(d, r);
         break;
      case 3:
         this.genMagicalChest(d, r, 684, 136, 22, 8);
         break;
      case 4:
         this.genFreeChest(d, r);
         break;
      case 5:
         this.genMagicalChest(d, r, 525, 52, 5, 9);
      }

      d.write(0);
      d.write(0);
      d.write(encodeVInt(634));
      d.write(this.type == 4 ? 2 : (this.type == 5 ? 9 : 8));
      this.data = b.toByteArray();
   }

   public void genTroop(DataOutputStream d, Random r, int type, int count) throws IOException {
      byte[] card = null;
      switch(type) {
      case 1:
         card = commons[r.nextInt(commons.length)];
         break;
      case 2:
         card = rares[r.nextInt(rares.length)];
         break;
      case 3:
         card = epics[r.nextInt(epics.length)];
         break;
      case 4:
         card = legendaries[r.nextInt(legendaries.length)];
      }

      if (this.was.contains(card)) {
         this.genTroop(d, r, type, count);
      } else {
         this.was.add(card);
         d.write(card[0]);
         d.write(card[1]);
         d.write(new byte[]{0, -117, -81, -53, 23});
         d.write(encodeVInt(count));
         d.write(new byte[3]);
      }
   }

   public void genGiantChest(DataOutputStream d, Random r) throws IOException {
      boolean hasEpic = r.nextBoolean();
      boolean hasLegendary = r.nextInt(20) == 2;
      d.write(hasEpic && hasLegendary ? 4 : 3);
      if (hasEpic) {
         this.genTroop(d, r, 3, 1);
         this.genTroop(d, r, 2, hasLegendary ? 30 : 31);
         this.genTroop(d, r, 1, 272);
         if (hasLegendary) {
            this.genTroop(d, r, 4, 1);
         }
      } else if (hasLegendary) {
         this.genTroop(d, r, 2, 30);
         this.genTroop(d, r, 1, 273);
         this.genTroop(d, r, 4, 1);
      } else {
         boolean isRareD = r.nextBoolean();
         int ri;
         if (isRareD) {
            ri = r.nextBoolean() ? 31 : 30;
            int i = r.nextInt(ri) + 1;
            this.genTroop(d, r, 1, 304 - ri);
            this.genTroop(d, r, 2, i);
            this.genTroop(d, r, 2, ri - i);
         } else {
            ri = r.nextInt(244) + 1;
            this.genTroop(d, r, 1, 274 - ri);
            this.genTroop(d, r, 2, 30);
            this.genTroop(d, r, 1, ri);
         }
      }

   }

   public void genFreeChest(DataOutputStream d, Random r) throws IOException {
      boolean hasEpic = r.nextInt(7) == 2;
      boolean hasTwoRaresSlots = hasEpic ? false : r.nextBoolean();
      boolean hasLegendary = r.nextInt(20) == 2;
      boolean hasTwoRares = !hasLegendary && !hasTwoRaresSlots ? r.nextBoolean() : false;
      int commons = !hasEpic || !hasTwoRares && !hasLegendary ? 10 : 9;
      d.write(hasEpic && hasLegendary ? 4 : (!hasEpic && !hasLegendary && !hasTwoRaresSlots ? 2 : 3));
      if (r.nextBoolean()) {
         this.genTroop(d, r, 2, hasTwoRares ? 2 : 1);
         this.genTroop(d, r, 1, commons);
      } else {
         this.genTroop(d, r, 1, commons);
         this.genTroop(d, r, 2, hasTwoRares ? 2 : 1);
      }

      if (hasEpic) {
         this.genTroop(d, r, 3, 1);
      }

      if (hasTwoRaresSlots) {
         this.genTroop(d, r, 2, 1);
      }

      if (hasLegendary) {
         this.genTroop(d, r, 4, 1);
      }

   }

   public void genMagicalChest(DataOutputStream d, Random r, int all, int rares, int epics, int slots) throws IOException {
      boolean hasLegendary = r.nextInt(10) == 2;
      int[] used = new int[3];
      int[] ost = new int[]{0, rares + r.nextInt(1), epics + r.nextInt(1)};
      ost[0] = all - ost[1] - ost[2] - (hasLegendary ? 1 : 0);
      d.write(encodeVInt(slots));
      int i = 0;

      while(i < slots - (hasLegendary ? 4 : 3)) {
         int st = r.nextBoolean() ? 0 : (r.nextBoolean() ? 1 : 2);
         if (ost[st] - used[st] > 3) {
            int num = r.nextInt(used[st] == 0 ? ost[st] / 2 : ost[st] - used[st] - 3) + 1;
            used[st] += num;
            this.genTroop(d, r, st + 1, num);
            ++i;
         }
      }

      if (r.nextBoolean()) {
         this.genTroop(d, r, 1, ost[0] - used[0]);
         this.genTroop(d, r, 2, ost[1] - used[1]);
      } else {
         this.genTroop(d, r, 2, ost[1] - used[1]);
         this.genTroop(d, r, 1, ost[0] - used[0]);
      }

      this.genTroop(d, r, 3, ost[2] - used[2]);
      if (hasLegendary) {
         this.genTroop(d, r, 4, 1);
      }

   }
}
