using System;
using System.IO;
using System.Security.Cryptography;
using CRS.Core;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000B1 RID: 177
	internal class LoginMessage : Message
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public LoginMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001B7AC File Offset: 0x000199AC
		public override void Decode()
		{
			if (base.Client.CState == 1)
			{
				try
				{
					using (PacketReader packetReader = new PacketReader(new MemoryStream(base.GetData())))
					{
						this.UserID = packetReader.ReadInt64();
						this.UserToken = packetReader.ReadString();
						this.Unknown = packetReader.ReadInt32();
						this.MasterHash = packetReader.ReadString();
						this.Unknown1 = packetReader.ReadString();
						this.OpenUDID = packetReader.ReadString();
						this.MacAddress = packetReader.ReadString();
						this.DeviceModel = packetReader.ReadString();
						this.AdvertisingGUID = packetReader.ReadString();
						this.OSVersion = packetReader.ReadString();
						this.Unknown2 = packetReader.ReadByte();
						this.Unknown3 = packetReader.ReadString();
						this.AndroidDeviceID = packetReader.ReadString();
						this.Language = packetReader.ReadString();
					}
				}
				catch (Exception e)
				{
					base.Client.CState = 0;
					Console.WriteLine(e);
				}
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001B8BC File Offset: 0x00019ABC
		public override void Process(Level a)
		{
			if (base.Client.CState == 0)
			{
				LoginFailedMessage loginFailedMessage = new LoginFailedMessage(base.Client);
				loginFailedMessage.SetErrorCode(10);
				loginFailedMessage.RemainingTime(30);
				PacketManager.Send(loginFailedMessage);
				return;
			}
			this.LoginClient();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001B8F4 File Offset: 0x00019AF4
		private void LogUser()
		{
			ResourcesManager.LogPlayerIn(this.level, base.Client);
			this.level.Tick();
			LoginOkMessage loginOkMessage = new LoginOkMessage(base.Client);
			ClientAvatar playerAvatar = this.level.GetPlayerAvatar();
			loginOkMessage.SetAccountId(playerAvatar.GetId());
			loginOkMessage.SetPassToken(this.UserToken);
			loginOkMessage.SetServerEnvironment("prod");
			loginOkMessage.SetDaysSinceStartedPlaying(10);
			loginOkMessage.SetServerTime(Math.Round(this.level.GetTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000.0).ToString());
			loginOkMessage.SetAccountCreatedDate("1414003838000");
			loginOkMessage.SetStartupCooldownSeconds(0);
			loginOkMessage.SetCountryCode(this.Language);
			PacketManager.Send(loginOkMessage);
			Alliance alliance = ObjectManager.GetAlliance(this.level.GetPlayerAvatar().GetAllianceId());
			PacketManager.Send(new OwnHomeDataMessage(base.Client, this.level));
			PacketManager.Send(new AvatarStreamMessage(base.Client, this.level));
			if (alliance != null)
			{
				PacketManager.Send(new AllianceStreamMessage(base.Client, alliance));
				PacketManager.Send(new AllianceDataMessage(base.Client, alliance));
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001BA30 File Offset: 0x00019C30
		private void LoginClient()
		{
			if (this.UserID == 0L || string.IsNullOrEmpty(this.UserToken))
			{
				this.NewUser();
				return;
			}
			this.level = ResourcesManager.GetPlayer(this.UserID, false);
			if (this.level == null)
			{
				this.NewUser();
				return;
			}
			if (this.level.Banned())
			{
				LoginFailedMessage loginFailedMessage = new LoginFailedMessage(base.Client);
				loginFailedMessage.SetErrorCode(11);
				PacketManager.Send(loginFailedMessage);
				return;
			}
			if (string.Equals(this.level.GetPlayerAvatar().GetUserToken(), this.UserToken, StringComparison.Ordinal))
			{
				this.LogUser();
				return;
			}
			LoginFailedMessage loginFailedMessage2 = new LoginFailedMessage(base.Client);
			loginFailedMessage2.SetErrorCode(11);
			loginFailedMessage2.SetReason("We have some problems with your account, please clean your app data.\nNous avons un problème avec votre compte, effacer les données de l'application puis réessayer.");
			PacketManager.Send(loginFailedMessage2);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001BAE8 File Offset: 0x00019CE8
		private void NewUser()
		{
			this.level = ObjectManager.CreateAvatar(0L, null);
			if (string.IsNullOrEmpty(this.UserToken))
			{
				byte[] array = new byte[20];
				new Random().NextBytes(array);
				using (SHA1 sha = new SHA1CryptoServiceProvider())
				{
					this.UserToken = BitConverter.ToString(sha.ComputeHash(array)).Replace("-", string.Empty);
				}
			}
			this.level.GetPlayerAvatar().SetToken(this.UserToken);
			DatabaseManager.Singelton.Save(this.level);
			this.LogUser();
		}

		// Token: 0x04000318 RID: 792
		public string AdvertisingGUID;

		// Token: 0x04000319 RID: 793
		public string AndroidDeviceID;

		// Token: 0x0400031A RID: 794
		public string DeviceModel;

		// Token: 0x0400031B RID: 795
		public string Language;

		// Token: 0x0400031C RID: 796
		public string MacAddress;

		// Token: 0x0400031D RID: 797
		public string MasterHash;

		// Token: 0x0400031E RID: 798
		public string OpenUDID;

		// Token: 0x0400031F RID: 799
		public string OSVersion;

		// Token: 0x04000320 RID: 800
		public int Unknown;

		// Token: 0x04000321 RID: 801
		public string Unknown1;

		// Token: 0x04000322 RID: 802
		public byte Unknown2;

		// Token: 0x04000323 RID: 803
		public string Unknown3;

		// Token: 0x04000324 RID: 804
		public long UserID;

		// Token: 0x04000325 RID: 805
		public string UserToken;

		// Token: 0x04000326 RID: 806
		public Level level;
	}
}
