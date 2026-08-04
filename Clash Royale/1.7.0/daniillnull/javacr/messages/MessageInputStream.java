package daniillnull.javacr.messages;

import daniillnull.javacr.cryptorc4.CryptoRC4;
import daniillnull.javacr.messages.client.ChangeAlliance;
import daniillnull.javacr.messages.client.CreateAlliance;
import daniillnull.javacr.messages.client.ExecuteCmd;
import daniillnull.javacr.messages.client.GetAllianceData;
import daniillnull.javacr.messages.client.GetHomeData;
import daniillnull.javacr.messages.client.GetJoinableAlliances;
import daniillnull.javacr.messages.client.GetProfileData;
import daniillnull.javacr.messages.client.CancelMatchmake;
import daniillnull.javacr.messages.client.CancelChallenge;
import daniillnull.javacr.messages.client.CancelTournament;
import daniillnull.javacr.messages.client.JoinAlliance;
import daniillnull.javacr.messages.client.KeepAlive;
import daniillnull.javacr.messages.client.LeaveAlliance;
import daniillnull.javacr.messages.client.Login;
import daniillnull.javacr.messages.client.PromoteAllianceMember;
import daniillnull.javacr.messages.client.SendAllianceMessage;
import daniillnull.javacr.messages.client.SessionReq;
import daniillnull.javacr.messages.client.SetName;
import daniillnull.javacr.messages.client.StartMission;
import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Arrays;

public class MessageInputStream {
   public DataInputStream is;
   public CryptoRC4 cr;

   public MessageInputStream(InputStream is, CryptoRC4 cr) {
      this.is = new DataInputStream(is);
      this.cr = cr;
   }

   public Packet read() throws IOException {
      int type = this.is.readChar();
      System.out.println("Gotcha " + type + " packet!");
      int length = this.is.read() << 16 | this.is.read() << 8 | this.is.read();
      char version = this.is.readChar();
      byte[] data = new byte[length];
      this.is.read(data);
      data = this.cr.decrypt(data, type);
      Packet p = null;
      switch(type) {
      case 10100:
         p = new SessionReq();
         break;
      case 10101:
         p = new Login();
         break;
      case 10108:
         p = new KeepAlive();
         break;
      case 10212:
         p = new SetName();
         break;
      case 14101:
         p = new GetHomeData();
         break;
      case 14102:
         p = new ExecuteCmd();
         break;
      case 14104:
         p = new StartMission();
         break;
      case 14107:
         p = new CancelMatchmake();
         break;
      case 14111:
         p = new CancelTournament();
         break;
      case 14113:
         p = new GetProfileData();
         break;
      case 14123:
         p = new CancelChallenge();
         break;
      case 14301:
         p = new CreateAlliance();
         break;
      case 14302:
         p = new GetAllianceData();
         break;
      case 14303:
         p = new GetJoinableAlliances();
         break;
      case 14305:
         p = new JoinAlliance();
         break;
      case 14306:
         p = new PromoteAllianceMember();
         break;
      case 14308:
         p = new LeaveAlliance();
         break;
      case 14315:
         p = new SendAllianceMessage();
         break;
      case 14316:
         p = new ChangeAlliance();
         break;
      default:
         System.out.println("Unhandled: " + type);
         System.out.println(Arrays.toString(data));
         return this.read();
      }

      ((Packet)p).id = type;
      ((Packet)p).version = version;
      ((Packet)p).data = data;
      ((Packet)p).process();
      return (Packet)p;
   }

   public void close() throws IOException {
      this.is.close();
   }
}
