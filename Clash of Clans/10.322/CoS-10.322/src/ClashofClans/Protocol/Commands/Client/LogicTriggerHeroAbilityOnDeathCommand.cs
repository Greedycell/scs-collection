using ClashofClans.Logic;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
	class LogicTriggerHeroAbilityOnDeathCommand : LogicCommand
	{
		public LogicTriggerHeroAbilityOnDeathCommand(Device device, IByteBuffer buffer) : base(device, buffer)
		{
		}
		private int State { get; set; }
		public override void Decode()
		{
			Reader.ReadInt();
			State = Reader.ReadByte();
		}

		public override void Process()
		{
			Device.Player.Home.Settings.SetTriggerHeroAbilityOnDeath(State);
		}
	}
}