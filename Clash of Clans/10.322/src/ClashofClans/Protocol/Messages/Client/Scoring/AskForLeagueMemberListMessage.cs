using ClashofClans.Logic;
using ClashofClans.Protocol.Messages.Server.Scoring;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Messages.Client.Scoring
{
	public class AskForLeagueMemberListMessage : PiranhaMessage
	{
		public AskForLeagueMemberListMessage(Device device, IByteBuffer buffer) : base(device, buffer)
		{
			RequiredState = Device.State.NotDefinied;
		}
		public override async void Process()
		{
			await new LeagueMemberListMessage(Device).SendAsync();
		}
	}
}