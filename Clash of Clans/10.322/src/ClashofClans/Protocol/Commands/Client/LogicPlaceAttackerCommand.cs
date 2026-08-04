using ClashofClans.Logic;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
	class LogicPlaceAttackerCommand : LogicCommand
	{
		public LogicPlaceAttackerCommand(Device device, IByteBuffer buffer) : base(device, buffer)
		{
		}
		private int UnitId { get; set; }
		public override void Decode()
		{
			Reader.ReadInt(); // X
			Reader.ReadInt(); // Y
			UnitId = Reader.ReadInt();

			/*Reader.ReadInt();
			UnitId = Reader.ReadInt();*/
			/*Reader.ReadVInt();
			Reader.ReadVInt();
			Reader.ReadBoolean();*/
		}
		public override void Process()
		{
			if (!Device.Player.Home.Battle.GetBattleStatus() && Device.CurrentBattleType == Device.BattleType.Multiplayer)
			{
				Device.Player.Home.Battle.StartBattle(Device);
			}

			if (Device.CurrentBattleType == Device.BattleType.Multiplayer || Device.CurrentBattleType == Device.BattleType.Goblins)
				Device.Player.Home.Units.RemoveTroop(UnitId);
		}
	}
}