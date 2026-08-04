using System;
using System.Collections.Generic;
using System.IO;
using ClashRoyaleProxy;
using UCS.Utilities.Sodium;

namespace UCS.PacketProcessing
{
	// Token: 0x0200008D RID: 141
	internal static class MessageFactory
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00012204 File Offset: 0x00010404
		static MessageFactory()
		{
			MessageFactory.m_vMessages.Add(10100, typeof(SessionRequest));
			MessageFactory.m_vMessages.Add(10101, typeof(LoginMessage));
			MessageFactory.m_vMessages.Add(10107, typeof(ClientCapabilitiesMessage));
			MessageFactory.m_vMessages.Add(10108, typeof(KeepAliveMessage));
			MessageFactory.m_vMessages.Add(10113, typeof(GetDeviceTokenMessage));
			MessageFactory.m_vMessages.Add(10212, typeof(ChangeAvatarNameMessage));
			MessageFactory.m_vMessages.Add(10513, typeof(AskForPlayingFacebookFriendsMessage));
			MessageFactory.m_vMessages.Add(10905, typeof(AskForNewsDataMessage));
			MessageFactory.m_vMessages.Add(14101, typeof(GoHomeMessage));
			MessageFactory.m_vMessages.Add(14102, typeof(ExecuteCommandsMessage));
			MessageFactory.m_vMessages.Add(14104, typeof(StartMissionMessage));
			MessageFactory.m_vMessages.Add(14105, typeof(HomeLogicStoppedMessage));
			MessageFactory.m_vMessages.Add(14107, typeof(AskForCancelAttackMessage));
			MessageFactory.m_vMessages.Add(14113, typeof(VisitHomeMessage));
			MessageFactory.m_vMessages.Add(14114, typeof(AskForBattleReplayMessage));
			MessageFactory.m_vMessages.Add(14302, typeof(AskForAllianceDataMessage));
			MessageFactory.m_vMessages.Add(14402, typeof(AskForTvContentMessage));
			MessageFactory.m_vMessages.Add(14405, typeof(AskForAvatarStreamEntryMessage));
			MessageFactory.m_vMessages.Add(14406, typeof(AskForBattleReplayStreamMessage));
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000123E0 File Offset: 0x000105E0
		public static object Read(Client c, BinaryReader br, int packetType)
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
			c.CRNonce = UCS.Utilities.Sodium.Utilities.Increment(UCS.Utilities.Sodium.Utilities.Increment(c.CRNonce));
			c.CSNonce = UCS.Utilities.Sodium.Utilities.Increment(UCS.Utilities.Sodium.Utilities.Increment(c.CSNonce));
			Console.WriteLine(string.Concat(new object[]
			{
				"[CRS]    The message '",
				PacketTypes.GetPacketTypeByID(packetType),
				" (",
				packetType,
				")' is unhandled"
			}));
			return null;
		}

		// Token: 0x0400026B RID: 619
		private static readonly Dictionary<int, Type> m_vMessages = new Dictionary<int, Type>();
	}
}
