using System;
using System.Collections.Generic;
using System.IO;
using CRS.Helpers;
using CRS.PacketProcessing.Commands;

namespace CRS.PacketProcessing
{
	// Token: 0x0200006C RID: 108
	internal static class CommandFactory
	{
		// Token: 0x06000329 RID: 809 RVA: 0x00017FC8 File Offset: 0x000161C8
		static CommandFactory()
		{
			CommandFactory.m_vCommands.Add(1U, typeof(PlaceUnitCommand));
			CommandFactory.m_vCommands.Add(501U, typeof(ChangeDeckCommand));
			CommandFactory.m_vCommands.Add(120U, typeof(NextCardCommand));
			CommandFactory.m_vCommands.Add(537U, typeof(SearchOppenentCommand));
			CommandFactory.m_vCommands.Add(506U, typeof(UnlockChestCommand));
			CommandFactory.m_vCommands.Add(516U, typeof(LevelUpCommand));
			CommandFactory.m_vCommands.Add(518U, typeof(OpenFreeChestCommand));
			CommandFactory.m_vCommands.Add(524U, typeof(RequestAllianceUnitsCommand));
			CommandFactory.m_vCommands.Add(528U, typeof(BuyChestCommand));
			CommandFactory.m_vCommands.Add(530U, typeof(BuyCardCommand));
			CommandFactory.m_vCommands.Add(534U, typeof(CreateChallengeCommand));
			CommandFactory.m_vCommands.Add(535U, typeof(ClaimAchievementCommand));
			CommandFactory.m_vCommands.Add(536U, typeof(TvReplaySeenCommand));
			CommandFactory.m_vCommands.Add(538U, typeof(ChestNextCardCommand));
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00018138 File Offset: 0x00016338
		public static object Read(BinaryReader br)
		{
			uint num = (uint)br.ReadVInt();
			if (CommandFactory.m_vCommands.ContainsKey(num))
			{
				return Activator.CreateInstance(CommandFactory.m_vCommands[num], new object[] { br });
			}
			Console.WriteLine(string.Concat(new object[]
			{
				"[GRS]    The command ",
				PacketTypes.GetPacketTypeByID((int)num),
				" is unhandled (",
				num,
				")"
			}));
			return null;
		}

		// Token: 0x040002B8 RID: 696
		private static readonly Dictionary<uint, Type> m_vCommands = new Dictionary<uint, Type>();
	}
}
