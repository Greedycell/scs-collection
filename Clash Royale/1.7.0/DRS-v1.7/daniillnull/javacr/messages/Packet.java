package daniillnull.javacr.messages;

import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStream;

public abstract class Packet {
   public int id;
   public byte[] data = new byte[0];
   public int version;

   public abstract void process() throws IOException;

   public static byte[] encodeVInt(int in) throws IOException {
      if (in < 64) {
         return new byte[]{(byte)(in & 63)};
      } else if (in < 8192) {
         return new byte[]{(byte)(in & 63 | 128), (byte)(in >> 6)};
      } else {
         return in < 1048576 ? new byte[]{(byte)(in & 63 | 128), (byte)(in >> 6 | 128), (byte)(in >> 13)} : new byte[1];
      }
   }

   public static int readVInt(InputStream br) throws IOException {
      int b = br.read();
      int num = b & 128;
      int num2 = b & 63;
      if ((b & 64) != 0) {
         if (num != 0) {
            b = br.read();
            num = b << 6 & 8128 | num2;
            if ((b & 128) != 0) {
               b = br.read();
               num |= b << 13 & 1040384;
               if ((b & 128) != 0) {
                  b = br.read();
                  num |= b << 20 & 133169152;
                  if ((b & 128) != 0) {
                     b = br.read();
                     num2 = num | b << 27 | Integer.MIN_VALUE;
                  } else {
                     num2 = num | -134217728;
                  }
               } else {
                  num2 = num | -1048576;
               }
            } else {
               num2 = num | -8192;
            }
         }
      } else if (num != 0) {
         b = br.read();
         num2 |= b << 6 & 8128;
         if ((b & 128) != 0) {
            b = br.read();
            num2 |= b << 13 & 1040384;
            if ((b & 128) != 0) {
               b = br.read();
               num2 |= b << 20 & 133169152;
               if ((b & 128) != 0) {
                  b = br.read();
                  num2 |= b << 27;
               }
            }
         }
      }

      return num2;
   }

   public static String readString(DataInputStream is) throws IOException {
      int len = is.readInt();
      if (len < 1) {
         return "";
      } else {
         byte[] tmp = new byte[len];
         is.read(tmp);
         return new String(tmp);
      }
   }
}
