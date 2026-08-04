using ClashofClans.Logic;
using ClashofClans.Protocol.Messages.Server.Battle;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicMatchmakingVillage2Command : LogicCommand
    {
        public LogicMatchmakingVillage2Command(Device device, IByteBuffer buffer) : base(device, buffer)
        {
        }

        public override async void Process()
        {
            Device.CurrentState = Device.State.Battle;
            Device.CurrentBattleType = Device.BattleType.Multiplayer;

            Device.Player.Home.GameMatchmakingManager.InitV2(Device);
        }
    }
}