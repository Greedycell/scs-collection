using ClashofClans.Logic;
using ClashofClans.Protocol.Messages.Server.Scoring;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Messages.Client.Scoring
{
	public class AskForAvatarRankingListMessage : PiranhaMessage
	{
		public AskForAvatarRankingListMessage(Device device, IByteBuffer buffer) : base(device, buffer)
		{
			RequiredState = Device.State.NotDefinied;
		}
		public override async void Process()
		{
			await new AvatarRankingListMessage(Device).SendAsync();
		}
	}
}