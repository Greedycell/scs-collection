using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000071 RID: 113
	internal class LoginMessage : Message
	{
		// Token: 0x06000330 RID: 816 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public LoginMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		public override void Decode()
		{
			if (base.Client.CState == 1)
			{
				using (CoCSharpPacketReader coCSharpPacketReader = new CoCSharpPacketReader(new MemoryStream(base.GetData())))
				{
					this.UserID = coCSharpPacketReader.ReadInt64();
					this.UserToken = coCSharpPacketReader.ReadString();
					this.Unknown = coCSharpPacketReader.ReadInt32();
					this.MasterHash = coCSharpPacketReader.ReadString();
					this.Unknown1 = coCSharpPacketReader.ReadString();
					this.OpenUDID = coCSharpPacketReader.ReadString();
					this.MacAddress = coCSharpPacketReader.ReadString();
					this.DeviceModel = coCSharpPacketReader.ReadString();
					this.AdvertisingGUID = coCSharpPacketReader.ReadString();
					this.OSVersion = coCSharpPacketReader.ReadString();
					this.Unknown2 = coCSharpPacketReader.ReadByte();
					this.Unknown3 = coCSharpPacketReader.ReadString();
					this.AndroidDeviceID = coCSharpPacketReader.ReadString();
					this.Language = coCSharpPacketReader.ReadString();
				}
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00010D94 File Offset: 0x0000EF94
		public override void Process(Level level)
		{
			if (Convert.ToBoolean(ConfigurationManager.AppSettings["maintenanceMode"]) || base.Client.CState == 0)
			{
				LoginFailedMessage loginFailedMessage = new LoginFailedMessage(base.Client);
				loginFailedMessage.SetErrorCode(10);
				PacketManager.ProcessOutgoingPacket(loginFailedMessage);
				return;
			}
			level = ResourcesManager.GetPlayer(this.UserID, false);
			if (level != null)
			{
				if (level.GetAccountStatus() == 99)
				{
					LoginFailedMessage loginFailedMessage2 = new LoginFailedMessage(base.Client);
					loginFailedMessage2.SetErrorCode(11);
					PacketManager.ProcessOutgoingPacket(loginFailedMessage2);
					return;
				}
			}
			else
			{
				level = ObjectManager.CreateAvatar(this.UserID);
				byte[] array = new byte[20];
				new Random().NextBytes(array);
				using (SHA1 sha = new SHA1CryptoServiceProvider())
				{
					this.UserToken = BitConverter.ToString(sha.ComputeHash(array)).Replace("-", string.Empty);
				}
			}
			if (Convert.ToBoolean(ConfigurationManager.AppSettings["useCustomPatch"]) && this.MasterHash != ObjectManager.FingerPrint.sha)
			{
				LoginFailedMessage loginFailedMessage3 = new LoginFailedMessage(base.Client);
				loginFailedMessage3.SetErrorCode(7);
				loginFailedMessage3.SetResourceFingerprintData(ObjectManager.FingerPrint.SaveToJson());
				loginFailedMessage3.SetContentURL(ConfigurationManager.AppSettings["patchingServer"]);
				loginFailedMessage3.SetUpdateURL("http://www.ultrapowa.com/client");
				PacketManager.ProcessOutgoingPacket(loginFailedMessage3);
				return;
			}
			base.Client.ClientSeed = this.Unknown;
			ResourcesManager.LogPlayerIn(level, base.Client);
			level.Tick();
			LoginOkMessage loginOkMessage = new LoginOkMessage(base.Client);
			ClientAvatar playerAvatar = level.GetPlayerAvatar();
			loginOkMessage.SetAccountId(playerAvatar.GetId());
			loginOkMessage.SetPassToken(this.UserToken);
			loginOkMessage.SetServerEnvironment("prod");
			loginOkMessage.SetDaysSinceStartedPlaying(10);
			loginOkMessage.SetServerTime(Math.Round(level.GetTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000.0).ToString());
			loginOkMessage.SetAccountCreatedDate("1414003838000");
			loginOkMessage.SetStartupCooldownSeconds(0);
			loginOkMessage.SetCountryCode(this.Language);
			PacketManager.ProcessOutgoingPacket(loginOkMessage);
			if (ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId()) == null)
			{
				level.GetPlayerAvatar().SetAllianceId(0L);
			}
			PacketManager.ProcessOutgoingPacket(new OwnHomeDataMessage(base.Client, level));
		}

		// Token: 0x0400021E RID: 542
		public string AdvertisingGUID;

		// Token: 0x0400021F RID: 543
		public string AndroidDeviceID;

		// Token: 0x04000220 RID: 544
		public string DeviceModel;

		// Token: 0x04000221 RID: 545
		public string Language;

		// Token: 0x04000222 RID: 546
		public string MacAddress;

		// Token: 0x04000223 RID: 547
		public string MasterHash;

		// Token: 0x04000224 RID: 548
		public string OpenUDID;

		// Token: 0x04000225 RID: 549
		public string OSVersion;

		// Token: 0x04000226 RID: 550
		public int Unknown;

		// Token: 0x04000227 RID: 551
		public string Unknown1;

		// Token: 0x04000228 RID: 552
		public byte Unknown2;

		// Token: 0x04000229 RID: 553
		public string Unknown3;

		// Token: 0x0400022A RID: 554
		public long UserID;

		// Token: 0x0400022B RID: 555
		public string UserToken;
	}
}
