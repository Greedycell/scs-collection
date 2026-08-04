using System;
using System.Collections.Generic;
using System.IO;
using CRS.Helpers;
using CRS.PacketProcessing.Messages.Client;

namespace CRS.PacketProcessing
{
	// Token: 0x0200006B RID: 107
	internal static class MessageFactory
	{
		// Token: 0x06000327 RID: 807 RVA: 0x00017BDC File Offset: 0x00015DDC
		static MessageFactory()
		{
			MessageFactory.m_vMessages.Add(10100, typeof(SessionRequest));
			MessageFactory.m_vMessages.Add(10101, typeof(LoginMessage));
			MessageFactory.m_vMessages.Add(10107, typeof(ClientCapabilitiesMessage));
			MessageFactory.m_vMessages.Add(10108, typeof(KeepAliveMessage));
			MessageFactory.m_vMessages.Add(10113, typeof(GetDeviceTokenMessage));
			MessageFactory.m_vMessages.Add(10121, typeof(UnlockAccountMessage));
			MessageFactory.m_vMessages.Add(10212, typeof(ChangeAvatarNameMessage));
			MessageFactory.m_vMessages.Add(10513, typeof(AskForPlayingFacebookFriendsMessage));
			MessageFactory.m_vMessages.Add(10905, typeof(AskForNewsDataMessage));
			MessageFactory.m_vMessages.Add(12903, typeof(RequestSectorMessage));
			MessageFactory.m_vMessages.Add(12904, typeof(SectorCommandMessage));
			MessageFactory.m_vMessages.Add(12951, typeof(SendBattleCommandMessage));
			MessageFactory.m_vMessages.Add(14101, typeof(GoHomeMessage));
			MessageFactory.m_vMessages.Add(14102, typeof(ExecuteCommandsMessage));
			MessageFactory.m_vMessages.Add(14104, typeof(StartMissionMessage));
			MessageFactory.m_vMessages.Add(14105, typeof(HomeLogicStoppedMessage));
			MessageFactory.m_vMessages.Add(14107, typeof(AskForCancelAttackMessage));
			MessageFactory.m_vMessages.Add(14113, typeof(AskForProfileDataMessage));
			MessageFactory.m_vMessages.Add(14114, typeof(AskForBattleReplayMessage));
			MessageFactory.m_vMessages.Add(14123, typeof(CancelChallengeMessage));
			MessageFactory.m_vMessages.Add(14301, typeof(CreateAllianceMessage));
			MessageFactory.m_vMessages.Add(14302, typeof(AskForAllianceDataMessage));
			MessageFactory.m_vMessages.Add(14303, typeof(AskForJoinableAllianceListMessage));
			MessageFactory.m_vMessages.Add(14305, typeof(AskForJoinAllianceMessage));
			MessageFactory.m_vMessages.Add(14308, typeof(LeaveAllianceMessage));
			MessageFactory.m_vMessages.Add(14315, typeof(ChatToAllianceStreamMessage));
			MessageFactory.m_vMessages.Add(14316, typeof(ChangeAllianceSettingMessage));
			MessageFactory.m_vMessages.Add(14324, typeof(SearchAlliancesMessage));
			MessageFactory.m_vMessages.Add(14401, typeof(TopGlobalAlliancesMessage));
			MessageFactory.m_vMessages.Add(14402, typeof(AskForTvContentMessage));
			MessageFactory.m_vMessages.Add(14403, typeof(TopGlobalPlayersMessage));
			MessageFactory.m_vMessages.Add(14404, typeof(TopLocalPlayersMessage));
			MessageFactory.m_vMessages.Add(14405, typeof(AskForAvatarStreamMessage));
			MessageFactory.m_vMessages.Add(14406, typeof(AskForBattleReplayStreamMessage));
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00017F48 File Offset: 0x00016148
		public static object Read(Device c, BinaryReader br, int packetType)
		{
			if (MessageFactory.m_vMessages.ContainsKey(packetType))
			{
				Console.WriteLine(string.Concat(new object[]
				{
					"[CRS]    Processing message ",
					PacketTypes.GetPacketTypeByID(packetType),
					" (",
					packetType,
					")"
				}));
				return Activator.CreateInstance(MessageFactory.m_vMessages[packetType], new object[] { c, br });
			}
			c.CSNonce.Increment();
			Console.WriteLine(string.Concat(new object[]
			{
				"[GRS]    The message '",
				PacketTypes.GetPacketTypeByID(packetType),
				" (",
				packetType,
				")' is unhandled"
			}));
			return null;
		}

		// Token: 0x040002B7 RID: 695
		private static readonly Dictionary<int, Type> m_vMessages = new Dictionary<int, Type>();
	}
}
