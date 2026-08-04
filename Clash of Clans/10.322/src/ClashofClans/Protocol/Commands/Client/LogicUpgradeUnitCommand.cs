using ClashofClans.Logic;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
	class LogicUpgradeUnitCommand : LogicCommand
	{
		public LogicUpgradeUnitCommand(Device device, IByteBuffer buffer) : base(device, buffer)
		{
		}

		private int UnitId { get; set; }
		private int UnitType { get; set; }

		public override void Decode()
		{
			Reader.ReadInt();

			UnitType = Reader.ReadInt();
			Reader.ReadInt();
			UnitId = Reader.ReadInt();
		}
		public override void Process()
		{
			Device.Player.Home.Units.Upgrade(UnitId, UnitType);
		}
	}
}