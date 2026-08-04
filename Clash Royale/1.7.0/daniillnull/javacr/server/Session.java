package daniillnull.javacr.server;

import daniillnull.javacr.cryptorc4.CryptoRC4;
import daniillnull.javacr.game.Alliance;
import daniillnull.javacr.game.AllianceChatMessage;
import daniillnull.javacr.game.Player;
import daniillnull.javacr.messages.MessageInputStream;
import daniillnull.javacr.messages.MessageOutputStream;
import daniillnull.javacr.messages.Packet;
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
import daniillnull.javacr.messages.server.AllianceCommandOk;
import daniillnull.javacr.messages.server.AllianceData;
import daniillnull.javacr.messages.server.AllianceMessageStream;
import daniillnull.javacr.messages.server.AllianceOnlinePlayersCount;
import daniillnull.javacr.messages.server.ChangeNameOk;
import daniillnull.javacr.messages.server.JoinableAlliances;
import daniillnull.javacr.messages.server.KeepAliveOk;
import daniillnull.javacr.messages.server.LoginOk;
import daniillnull.javacr.messages.server.OwnHomeData;
import daniillnull.javacr.messages.server.ProfileData;
import daniillnull.javacr.messages.server.CancelMatchmakeDone;
import daniillnull.javacr.messages.server.CancelChallengeDone;
import daniillnull.javacr.messages.server.SectorState;
import daniillnull.javacr.messages.server.SessionOk;
import daniillnull.javacr.messages.server.UpdateResources;
import java.io.EOFException;
import java.io.IOException;
import java.net.SocketException;

public class Session implements Runnable {
   public Thread curr;
   public MessageInputStream is;
   public MessageOutputStream os;
   public CryptoRC4 cr;
   public Player player;

   public void run() {
      try {
         this.work();
      } catch (Exception var4) {
         if (!(var4 instanceof IOException) && !(var4 instanceof SocketException) && !(var4 instanceof EOFException)) {
            var4.printStackTrace();
         }

         try {
            Main.sessions.remove(this);
            this.is.close();
            this.os.close();
            this.player.current = null;
         } catch (Exception var3) {
         }
      }

   }

   public void work() throws Exception {
      //SessionReq rr = (SessionReq)this.is.read();
      //this.os.write(new SessionOk());
      Login login = (Login)this.is.read();
      /*if (!Main.fingerPrintHash.isEmpty() && !rr.rhash.equals(Main.fingerPrintHash)) {
         this.os.write(new UpdateResources());
      } else {*/
         this.player = Player.load(login.vid);
         this.player.current = this;
         this.os.write(new LoginOk(this.player.id));
         this.os.write(new OwnHomeData(this.player));
         if (this.player.kickedfrom != 0) {
            this.os.write(new AllianceCommandOk(Alliance.load(this.player.kickedfrom), 141, true));
            this.player.kickedfrom = 0;
         }

         if (this.player.all != null) {
            this.os.write(new AllianceMessageStream(this.player.all.messages));
         }

         while(!Thread.interrupted()) {
            Packet pk = this.is.read();
            if (pk instanceof KeepAlive) {
               this.os.write(new KeepAliveOk());
            }

            if (pk instanceof GetProfileData) {
               GetProfileData m = (GetProfileData)pk;
               this.os.write(new ProfileData(Player.load(m.uid)));
            }

            if (pk instanceof CancelMatchmake) {
               CancelMatchmake m = (CancelMatchmake)pk;
               this.os.write(new CancelMatchmakeDone());
            }

            if (pk instanceof CancelChallenge) {
               CancelChallenge m = (CancelChallenge)pk;
               this.os.write(new CancelChallengeDone());
            }

            if (pk instanceof CancelTournament) {
               CancelTournament m = (CancelTournament)pk;
               this.os.write(new CancelMatchmakeDone());
            }

            if (pk instanceof GetAllianceData) {
               GetAllianceData m = (GetAllianceData)pk;
               this.os.write(new AllianceData(Alliance.load(m.cid)));
               if (this.player.all != null) {
                  this.os.write(new AllianceOnlinePlayersCount(this.player.all));
               }
            }

            if (pk instanceof ChangeAlliance) {
               ChangeAlliance m = (ChangeAlliance)pk;
               this.player.all.descr = m.descr;
               this.player.all.badge = m.badge;
               this.player.all.reqsc = m.reqsc;
               this.player.all.orig = m.orig;
               this.os.write(new AllianceData(this.player.all));
            }

            if (pk instanceof PromoteAllianceMember) {
               PromoteAllianceMember m = (PromoteAllianceMember)pk;
               Player pl = Player.load(m.uid);
               if (this.player.all == pl.all && (this.player.role == 2 || m.role != 2)) {
                  int[] roles = new int[]{1, 1, 4, 2, 3};
                  int old = pl.role;
                  pl.role = m.role;
                  this.player.all.addMessage(AllianceChatMessage.createEvent(roles[old] > roles[m.role] ? 6 : 5, pl, this.player.name));
                  if (m.role == 2) {
                     this.player.role = 4;
                     this.player.all.addMessage(AllianceChatMessage.createEvent(6, this.player, this.player.name));
                  }
               }
            }

            AllianceChatMessage message;
            if (pk instanceof SendAllianceMessage) {
               SendAllianceMessage m = (SendAllianceMessage)pk;
               message = AllianceChatMessage.createText(m.message, this.player);
               this.player.all.addMessage(message);
            }

            if (pk instanceof CreateAlliance) {
               CreateAlliance m = (CreateAlliance)pk;
               this.player.all = Alliance.load(0);
               this.player.all.players.add(this.player);
               this.player.role = 2;
               this.player.all.name = m.name;
               this.player.all.descr = m.descr;
               this.player.all.badge = m.badge;
               this.player.all.reqsc = m.reqsc;
               this.player.all.orig = m.orig;
               this.os.write(new AllianceCommandOk(this.player.all, 142, true));
            }

            if (pk instanceof JoinAlliance) {
               JoinAlliance m = (JoinAlliance)pk;
               message = AllianceChatMessage.createEvent(3, this.player, "");
               this.player.all = Alliance.load(m.cid);
               this.player.all.addMessage(message);
               this.player.all.players.add(this.player);
               this.player.role = 1;
               this.os.write(new AllianceCommandOk(this.player.all, 142, false));
               this.os.write(new AllianceMessageStream(this.player.all.messages));
            }

            if (pk instanceof LeaveAlliance) {
               AllianceChatMessage message_0 = AllianceChatMessage.createEvent(4, this.player, "");
               this.player.all.players.remove(this.player);
               this.player.all.addMessage(message_0);
               if (this.player.role == 2 && this.player.all.players.size() > 0) {
                  ((Player)this.player.all.players.get(0)).role = 2;
               }

               this.os.write(new AllianceCommandOk(this.player.all, 141, false));
               this.player.all = null;
            }

            if (pk instanceof GetJoinableAlliances) {
               this.os.write(new JoinableAlliances());
            }

            if (pk instanceof ExecuteCmd) {
               ExecuteCmd m = (ExecuteCmd)pk;
               m.executeAll(this);
            }

            if (pk instanceof StartMission) {
               this.os.write(new SectorState(this.player, 0));
            }

            if (pk instanceof GetHomeData) {
               this.os.write(new OwnHomeData(this.player));
            }

            if (pk instanceof SetName) {
               SetName m = (SetName)pk;
               this.player.name = m.name;
               this.os.write(new ChangeNameOk(m.name));
            }
         }

      //}
   }
}
