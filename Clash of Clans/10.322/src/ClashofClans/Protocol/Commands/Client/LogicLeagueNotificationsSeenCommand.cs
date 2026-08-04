using ClashofClans.Logic;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
	class LogicLeagueNotificationsSeenCommand : LogicCommand
	{
		public LogicLeagueNotificationsSeenCommand(Device device, IByteBuffer buffer) : base(device, buffer)
		{
		}

		public override void Decode()
		{
			Reader.ReadInt();
			Reader.ReadInt();
			Reader.ReadInt();
		}

		public override void Process()
		{
			Device.Player.Home.GameObjectManager.LastLeagueRank = Device.Player.Home.League;
		}
	}
}