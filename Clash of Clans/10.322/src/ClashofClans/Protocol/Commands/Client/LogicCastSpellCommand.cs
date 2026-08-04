using ClashofClans.Logic;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
	class LogicCastSpellCommand : LogicCommand
	{
		public LogicCastSpellCommand(Device device, IByteBuffer buffer) : base(device, buffer)
		{
		}
		private int SpellId { get; set; }
		public override void Decode()
		{
			Reader.ReadInt();
			SpellId = Reader.ReadInt();

			/*Reader.ReadVInt();
			Reader.ReadVInt();
			Reader.ReadBoolean();
			Reader.ReadVInt();*/
		}
		public override void Process()
		{
			if (!Device.Player.Home.Battle.GetBattleStatus() && Device.CurrentBattleType == Device.BattleType.Multiplayer)
			{
				Device.Player.Home.Battle.StartBattle(Device);
			}

			if (Device.CurrentBattleType == Device.BattleType.Multiplayer || Device.CurrentBattleType == Device.BattleType.Goblins)
				Device.Player.Home.Units.RemoveSpell(SpellId);
		}
	}
}