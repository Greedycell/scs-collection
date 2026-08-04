using System;
using System.Collections.Generic;
using System.IO;
using ClashRoyaleProxy;
using UCS.Helpers;
using UCS.PacketProcessing.Commands;

namespace UCS.PacketProcessing
{
	// Token: 0x0200008E RID: 142
	internal static class CommandFactory
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x000124BC File Offset: 0x000106BC
		static CommandFactory()
		{
			CommandFactory.m_vCommands.Add(120U, typeof(NextCardCommand));
			CommandFactory.m_vCommands.Add(537U, typeof(SearchOppenentCommand));
			CommandFactory.m_vCommands.Add(506U, typeof(UnlockChestCommand));
			CommandFactory.m_vCommands.Add(516U, typeof(LevelUpCommand));
			CommandFactory.m_vCommands.Add(528U, typeof(BuyChestCommand));
			CommandFactory.m_vCommands.Add(530U, typeof(BuyCardCommand));
			CommandFactory.m_vCommands.Add(535U, typeof(ClaimAchievementCommand));
			CommandFactory.m_vCommands.Add(536U, typeof(TvReplaySeenCommand));
			CommandFactory.m_vCommands.Add(538U, typeof(ChestNextCardCommand));
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000125B4 File Offset: 0x000107B4
		public static object Read(BinaryReader br)
		{
			uint num = (uint)br.ReadVInt();
			if (CommandFactory.m_vCommands.ContainsKey(num))
			{
				Console.WriteLine(string.Concat(new object[]
				{
					"[CRS]    Processing command ",
					PacketTypes.GetPacketTypeByID((int)num),
					" (",
					num,
					")"
				}));
				return Activator.CreateInstance(CommandFactory.m_vCommands[num], new object[] { br });
			}
			Console.WriteLine("[CRS]    The command " + num + " is unhandled");
			return null;
		}

		// Token: 0x0400026C RID: 620
		private static readonly Dictionary<uint, Type> m_vCommands = new Dictionary<uint, Type>();
	}
}
