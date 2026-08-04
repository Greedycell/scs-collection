package daniillnull.javacr.server;

import daniillnull.javacr.cryptorc4.CryptoRC4;
import daniillnull.javacr.game.Alliance;
import daniillnull.javacr.game.Player;
import daniillnull.javacr.messages.MessageInputStream;
import daniillnull.javacr.messages.MessageOutputStream;
import java.io.FileInputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Arrays;
import java.util.LinkedList;
import java.util.Timer;
import java.util.TimerTask;

public class Main {
   public static LinkedList<Session> sessions;
   public static byte[] fingerPrint = new byte[80000];
   public static String fingerPrintUrl;
   public static String fingerPrintHash;

   public static void main(String[] args) throws Exception {
      fingerPrintUrl = args.length > 1 ? args[0] : "";
      fingerPrint = args.length > 1 ? Arrays.copyOf(fingerPrint, (new FileInputStream(args[1])).read(fingerPrint)) : null;
      fingerPrintHash = (new String(Arrays.copyOfRange(fingerPrint, (new String(fingerPrint)).indexOf("],\"sha\":") + 9, fingerPrint.length))).substring(0, 40);
      System.out.println("Server starting...");
      sessions = new LinkedList();
      Player.loadSavedData();
      Alliance.loadSavedData();
      (new Timer(true)).schedule(new TimerTask() {
         public void run() {
            try {
               Player.saveData();
               Alliance.saveData();
            } catch (Exception var2) {
               var2.printStackTrace();
            }

         }
      }, 1000L, 20000L);
      ServerSocket ss = new ServerSocket(9339);
      System.out.println("Server started on port 9339!");

      while(ss.isBound()) {
         Socket s = ss.accept();
         Session z = new Session();
         z.curr = new Thread(z);
         z.cr = new CryptoRC4();
         z.is = new MessageInputStream(s.getInputStream(), z.cr);
         z.os = new MessageOutputStream(s.getOutputStream(), z.cr);
         z.curr.start();
         sessions.add(z);
      }

      ss.close();
   }
}
