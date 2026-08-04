using ClashofClans.Logic;
using ClashofClans.Protocol.Messages.Server.Scoring;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Messages.Client.Scoring
{
	public class AskForAllianceRankingListMessage : PiranhaMessage
	{
		public AskForAllianceRankingListMessage(Device device, IByteBuffer buffer) : base(device, buffer)
		{
			RequiredState = Device.State.NotDefinied;
		}
		public bool LocalRanking { get; set; }

		public override void Decode()
		{
			if (Reader.ReadBoolean())
			{
				Reader.ReadLong();
			}
			LocalRanking = Reader.ReadBoolean();
		}
		public override async void Process()
		{
			if (LocalRanking)
				await new AllianceLocalRankingListMessage(Device).SendAsync();
			else
				await new AllianceRankingListMessage(Device).SendAsync();
		}
	}
}