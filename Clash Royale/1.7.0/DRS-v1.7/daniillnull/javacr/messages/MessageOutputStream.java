package daniillnull.javacr.messages;

import daniillnull.javacr.cryptorc4.CryptoRC4;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.OutputStream;

public class MessageOutputStream {
   public DataOutputStream is;
   public CryptoRC4 cr;

   public MessageOutputStream(OutputStream is, CryptoRC4 cr) {
      this.is = new DataOutputStream(is);
      this.cr = cr;
   }

   public void write(Packet p) throws IOException {
      p.process();

      if (p.data == null) {
         System.err.println("Packet " + p.id + " has null data, skipping write.");
         return;
      }

      byte[] encrypted = this.cr.encrypt(p.data, p.id);
      if (encrypted == null) {
         System.err.println("Encryption failed for packet " + p.id);
         return;
      }

      p.data = encrypted;

      this.is.writeChar((char)p.id);

      int len = p.data.length;
      this.is.write((len >>> 16) & 0xFF);
      this.is.write((len >>> 8) & 0xFF);
      this.is.write(len & 0xFF);

      this.is.writeChar(p.version);
      this.is.write(p.data);
   }

   public void close() throws IOException {
      this.is.close();
   }
}
