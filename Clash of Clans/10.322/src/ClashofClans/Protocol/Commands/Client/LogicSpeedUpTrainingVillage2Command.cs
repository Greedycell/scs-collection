using ClashofClans.Logic;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
	class LogicSpeedUpTrainingVillage2Command : LogicCommand
	{
		public LogicSpeedUpTrainingVillage2Command(Device device, IByteBuffer buffer) : base(device, buffer)
		{
		}

		public override void Decode()
		{
		}
		public override void Process()
		{
		}
	}
}